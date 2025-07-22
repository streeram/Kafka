using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Shared;
using System;
using System.Threading;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;

namespace Consumer;

public class KafkaConsumer
{
    private readonly string _topic;
    private readonly string _bootstrapServers;
    private IConsumer<Ignore, MyRecord> _consumer;
    private readonly string _schemaRegistryUrl = "http://localhost:8081"; // Update as needed

    public KafkaConsumer(string topic, string bootstrapServers)
    {
        _topic = topic;
        _bootstrapServers = bootstrapServers;
        ConfigureConsumer();
    }

    private void ConfigureConsumer()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _bootstrapServers,
            GroupId = "consumer-group-" + Guid.NewGuid(),
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var schemaRegistryConfig = new SchemaRegistryConfig
        {
            Url = _schemaRegistryUrl
        };

        var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
        _consumer = new ConsumerBuilder<Ignore, MyRecord>(config)
            .SetValueDeserializer(new JsonDeserializer<MyRecord>(schemaRegistry).AsSyncOverAsync())
            .Build();
    }

    public void ConsumeMessages(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(_topic);

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var cr = _consumer.Consume(cancellationToken);
                var message = cr.Message.Value;
                Console.WriteLine($"Received: Id={message.Id}, Name={message.Name}");
            }
        }
        catch (ConsumeException e)
        {
            Console.WriteLine($"Error occurred: {e.Error.Reason}");
        }
    }
}