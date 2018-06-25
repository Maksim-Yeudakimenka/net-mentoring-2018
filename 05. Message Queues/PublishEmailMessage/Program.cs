using System;
using EasyNetQ;
using EasyNetQ.Topology;
using EmailSender.CommonTypes;
using EmailSender.Core;

namespace PublishEmailMessage
{
  class Program
  {
    static void Main(string[] args)
    {
      var configuration = new RabbitMqConfiguration();

      var rabbitMqBus = ApplicationBootstrapper.ConfigureRabbitMqBus(configuration);
      var exchangeName = configuration.ExchangeName;
      var routingKey = configuration.RoutingKey;
      var advancedRabbitMqBus = rabbitMqBus.Advanced;

      var exchange = advancedRabbitMqBus.ExchangeDeclare(exchangeName, ExchangeType.Topic);

      var emailMessage = new EmailMessage
      {
        Id = 1,
        Subject = "Test subject",
        Body = "Some body",
        Recipient = "vasiliy@example.com"
      };

      var message = new Message<EmailMessage>(emailMessage);

      try
      {
        Console.WriteLine("Publishing message...");
        advancedRabbitMqBus.Publish(exchange, routingKey, false, message);
      }
      catch (Exception e)
      {
        Console.WriteLine("Publish failed. " + e.Message);
        throw;
      }

      Console.WriteLine("Publish succeeded.");

      rabbitMqBus.Dispose();
    }
  }
}
