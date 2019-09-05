using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace rabbit_receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("samplequeue", true, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (m, i) =>
                    {
                        var message = Encoding.UTF8.GetString(i.Body);
                        Console.WriteLine($"Received message from queue: {message}");
                    };

                    channel.BasicConsume("samplequeue", true, consumer);
                    Console.ReadKey();
                }
            }
        }
    }
}
