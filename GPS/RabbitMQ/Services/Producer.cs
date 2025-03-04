using System.Text;
using GPS.RabbitMQ.Services.Interfaces;
using RabbitMQ.Client;

namespace GPS.RabbitMQ{

    public class Producer : IProducer {
        
        public async Task<string> ProduceAsync(string message) {
            var factory = new ConnectionFactory{
                UserName = "admin",
                Password = "admin",
                VirtualHost = "/",
                HostName = "rabbitmq"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "Test", 
                durable: false, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null
            );
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: "Test",
                body: body
            );

           return $" [x] Sent {message}";
        }
    }
    
}