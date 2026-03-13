using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;
using SignalDeck.Sdk.Models;

namespace SignalDeck.Sdk
{
    public class SignalDeckClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Channel<SignalEvent> _channel;
        private readonly CancellationTokenSource _cts = new();
        private readonly Task _workerTask;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

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

            // Retry 3 times if the status code is 5xx or a network failure
            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => (int)r.StatusCode >= 500)
                .WaitAndRetryAsync(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                );
        }

        public void EnqueueSignal(SignalEvent @event)
        {
            _channel.Writer.TryWrite(@event);
        }

        public async Task ProcessQueueAsync()
        {
            var batch = new List<SignalEvent>();
            
            await foreach(var @event in _channel.Reader.ReadAllAsync(_cts.Token))
            {
                batch.Add(@event);

                if (batch.Count >= 10)
                {
                    await SendBatchAsync(batch);
                    batch.Clear();
                }
            }

            // Send any remaining signals in the batch before exiting
            if (batch.Any())
            {
                await SendBatchAsync(batch);
            }
        }

        private async Task SendBatchAsync(List<SignalEvent> batch)
        {
            await _retryPolicy.ExecuteAsync(async () =>
             {
                var response = await _httpClient.PostAsJsonAsync("api/v1/ingestion/batch", batch, _cts.Token);
                if (!response.IsSuccessStatusCode)
                {
                    Console.Error.WriteLine($"Failed to send batch: {response.StatusCode}");
                }
                return response;
             });
        }

        public void Dispose()
        {
            _channel.Writer.Complete();

            _workerTask.Wait(TimeSpan.FromSeconds(5));

            _cts.Cancel();
            _httpClient.Dispose();
        }
    }
}