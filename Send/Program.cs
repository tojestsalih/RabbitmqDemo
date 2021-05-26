using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var connFactory = new ConnectionFactory { HostName = "localhost" , Port  =8081 };
            using (var connection = connFactory.CreateConnection())
            
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    string message = args[0];

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "hello",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine($"[x] Sent {message}");
                    

                }
                Console.WriteLine("Press enter to exit");
                Console.WriteLine();
            }
        
    }
}
