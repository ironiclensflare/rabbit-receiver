using System;
using System.Text;
using ironiclensflare.logger;
using log4net;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace rabbit_receiver
{
    class Program
    {
        private static readonly ILog _logger = Logger.GetLogger();

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                _logger.Info("Connecting to RabbitMQ");
                using (var channel = connection.CreateModel())
                {
                    _logger.Debug("Declaring queue");
                    channel.QueueDeclare("samplequeue", true, false, false, null);

                    _logger.Debug("Setting up consumer");
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (m, i) =>
                    {
                        var message = Encoding.UTF8.GetString(i.Body);
                        _logger.Info($"Received message from queue: {message}");
                    };

                    channel.BasicConsume("samplequeue", true, consumer);
                    Console.ReadKey();
                }
            }
        }
    }
}
