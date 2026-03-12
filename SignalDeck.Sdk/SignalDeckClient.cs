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
            await foreach(var @event in _channel.Reader.ReadAllAsync(_cts.Token))
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync("api/v1/ingestion", @event, _cts.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        // Log the error or handle it as needed
                        Console.Error.WriteLine($"Failed to send signal: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    Console.Error.WriteLine($"Exception while sending signal: {ex.Message}");

                    // TODO: Implement retry logic
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