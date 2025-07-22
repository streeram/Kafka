using System;
using System.Threading.Tasks;
using Shared;

namespace Producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var kafkaProducer = new KafkaProducer("my-topic");

            Console.WriteLine("Enter messages to send to Kafka (type 'exit' to quit):");
            string message;
            while ((message = Console.ReadLine()) != "exit")
            {
                await kafkaProducer.SendMessage(new MyRecord
                {
                    Id = new Random().Next(1, 1000), // Random ID for demonstration
                    Name = message // Use the input message as the Name
                });
                Console.WriteLine($"Sent: {message}");
            }
        }
    }
}