# Kafka .NET Application

This project is a .NET 9 application that demonstrates how to use Apache Kafka for messaging. It includes both a Kafka producer and a consumer, allowing you to send and receive messages to and from a Kafka topic.

## Project Structure

```
dotnet-kafka-app
├── src
│   ├── Producer
│   │   ├── Producer.csproj
│   │   ├── Program.cs
│   │   └── KafkaProducer.cs
│   ├── Consumer
│   │   ├── Consumer.csproj
│   │   ├── Program.cs
│   │   └── KafkaConsumer.cs
│   └── Shared
│       └── SharedModels.cs
├── dotnet-kafka-app.sln
└── README.md
```

## Getting Started

### Prerequisites

- .NET 9 SDK
- Apache Kafka and Zookeeper running locally or accessible remotely

### Setup

1. Clone the repository:
   ```
   git clone <repository-url>
   cd dotnet-kafka-app
   ```

2. Restore the dependencies for both the producer and consumer projects:
   ```
   dotnet restore src/Producer/Producer.csproj
   dotnet restore src/Consumer/Consumer.csproj
   ```

3. Build the solution:
   ```
   dotnet build dotnet-kafka-app.sln
   ```

### Running the Producer

To run the Kafka producer, navigate to the Producer directory and execute the following command:

```
dotnet run --project src/Producer/Producer.csproj
```

You can send messages to a specified Kafka topic using the producer.

### Running the Consumer

To run the Kafka consumer, navigate to the Consumer directory and execute the following command:

```
dotnet run --project src/Consumer/Consumer.csproj
```

The consumer will listen for messages from a specified Kafka topic and process them accordingly.

## Contributing

Feel free to submit issues or pull requests for improvements or bug fixes.

## License

This project is licensed under the MIT License.