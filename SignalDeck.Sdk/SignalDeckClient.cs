using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using SignalDeck.Sdk.Models;

namespace SignalDeck.Sdk
{
    public class SignalDeckClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Channel<SignalEvent> _channel;
        private readonly CancellationTokenSource _cts = new();
        private readonly Task _workerTask;

        public SignalDeckClient(string apiKey, string baseUrl = "https://api.signaldeck.com")
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("X-Signal-Key", apiKey);

            // Create a Bounded channel so we don't crash the user's app if the API is down
            // If the queue hits 10,000, it will start dropping oldest signals (Lossy)
            _channel = Channel.CreateBounded<SignalEvent>(new BoundedChannelOptions(10000)
            {
                FullMode = BoundedChannelFullMode.DropOldest
            });

            _workerTask = Task.Run(ProcessQueueAsync);
        }

        public void EnqueueSignal(SignalEvent @event)
        {
            _channel.Writer.TryWrite(@event);
        }

        public async Task ProcessQueueAsync()
        {
            var batch = new List<SignalEvent>();
            var batchSize = 10;
            var maxWaitTime = TimeSpan.FromSeconds(5);

            while (!_cts.Token.IsCancellationRequested)
            {
                try
                {
                    if (await _channel.Reader.WaitToReadAsync(_cts.Token))
                    {
                        while (batch.Count < batchSize && _channel.Reader.TryRead(out var @event) && (DateTime.UtcNow - batch.FirstOrDefault()?.Timestamp) < maxWaitTime)
                        {
                            batch.Add(@event);
                        }

                        if (batch.Any())
                        {
                            await _httpClient.PostAsJsonAsync("api/v1/ingestion/batch", batch, _cts.Token);
                            batch.Clear();
                        }
                    }
                }
                catch (OperationCanceledException) { break; }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    Console.Error.WriteLine($"Exception while processing queue: {ex.Message}");
                }
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
            _channel.Writer.Complete();
        }
    }
}