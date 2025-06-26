using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Publisher
{
    public class Producer : BackgroundService
    {
        private readonly ILogger<Producer> _logger;
        private static readonly string ConnectionString = "localhost:6379";
        private static readonly ConnectionMultiplexer Connection = ConnectionMultiplexer.Connect(ConnectionString);
        private const string Channel = "test-channel";
        public Producer(ILogger<Producer> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = Connection.GetSubscriber();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Producer is running at: {time}", DateTimeOffset.Now);
                await subscriber.PublishAsync(Channel, "Test message...");
                await Task.Delay(1000, stoppingToken); // Simulate work
            }
        }
    }
}
