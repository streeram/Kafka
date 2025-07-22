using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Confluent.SchemaRegistry;
using Shared;
using System;
using System.Threading.Tasks;

namespace Producer
{
    public class KafkaProducer
    {
        private readonly IProducer<Null, MyRecord> _producer;
        private readonly string _topic;
        private readonly string _schemaRegistryUrl = "http://localhost:8081"; // Update as needed

        public KafkaProducer(string topic)
        {
            _topic = topic;
            _producer = ConfigureProducer();
        }

        private IProducer<Null, MyRecord> ConfigureProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = _schemaRegistryUrl
            };
            var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
            return new ProducerBuilder<Null, MyRecord>(config)
                .SetValueSerializer(new JsonSerializer<MyRecord>(schemaRegistry))
                .Build();
        }

        public async Task SendMessage(MyRecord message)
        {
            try
            {
                var result = await _producer.ProduceAsync(_topic, new Message<Null, MyRecord> { Value = message });
                Console.WriteLine($"Message sent to {result.Topic} partition {result.Partition} with offset {result.Offset}");
            }
            catch (ProduceException<Null, MyRecord> e)
            {
                Console.WriteLine($"Failed to deliver message: {e.Error.Reason}");
            }
        }
    }
}