using System.Threading;
using Consumer;

class Program
{
    private static string bootstrapServers = "localhost:9092"; // Update with your Kafka server
    private static string topic = "my-topic"; // Update with your Kafka topic

    static void Main(string[] args)
    {
        var consumer = new KafkaConsumer(topic, bootstrapServers);
        consumer.ConsumeMessages(CancellationToken.None);
    }
}