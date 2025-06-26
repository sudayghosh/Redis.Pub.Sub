using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Subscriber
{
    public class Consumer : BackgroundService
    {
        private static readonly string ConnectionString = "localhost:6379";
        private static readonly ConnectionMultiplexer Connection = ConnectionMultiplexer.Connect(ConnectionString);
        private const string Channel = "test-channel";

        private readonly ILogger<Consumer> _logger;
        public Consumer(ILogger<Consumer> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Consumer is running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken); // Simulate work
            //}

            var subscriber = Connection.GetSubscriber();
            subscriber.Subscribe(Channel, (channel, message) =>
            {
                _logger.LogInformation($"Received message: {message} on channel: {channel}");
            });
        }
    }
}
