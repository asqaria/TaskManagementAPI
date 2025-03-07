using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Serilog;
using TaskManagementAPI.Services.Interface;

namespace TaskManagementAPI.Services {
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ConnectionFactory factory;

        public RabbitMQService(){
            factory = new ConnectionFactory(){
                HostName = "localhost"
            };
        }

        public void SendMessage<T>(T message, string queueName) {
            using var connection = this.factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            Log.Information($"[x] Sent message to {queueName}");
        }
    }   
}
