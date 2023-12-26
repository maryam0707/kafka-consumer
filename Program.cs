using System;
using Confluent.Kafka;
using kafka_output;

class Program
{
    static void Main()
    {
        Console.WriteLine("Kafka terminal launch");
        var config = kafka_output.kafkaConsumerConfig.GetConfig();
        string topic = "onlineShopping"; 
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        consumer.Subscribe(topic);
        while (true)
        {
            try
            {
                var consumeResult = consumer.Consume();
                Console.WriteLine($"Received message: {consumeResult.Message.Value}");

                
                consumer.Commit(consumeResult);
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error consuming message: {e.Error.Reason}");
            }
        }
    }
}