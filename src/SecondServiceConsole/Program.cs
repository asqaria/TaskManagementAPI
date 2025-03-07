using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

class Program {
    static async Task Main()
    {
        var factory = new ConnectionFactory(){
            HostName = "localhost"
        };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "createdTasks", durable: false, exclusive: false, autoDelete: false, arguments: null);
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"[x] Recieved: {message}");
            await ProcessMessageAsync(message);
        };
        
        channel.BasicConsume(queue: "createdTasks", autoAck: true, consumer: consumer);
        Console.WriteLine("[x] Waiting for messages.........");
        await Task.Delay(-1);
    }

    private static async Task ProcessMessageAsync(string message){
        await Task.Delay(500);
        Console.WriteLine($"[+] Processed: {message}");
    }
}