using System;
using EasyNetQ;
using EasyNetQ.Topology;
using EmailSender.CommonTypes;
using EmailSender.Core;

namespace EmailSender.Smtp
{
  public class EmailSenderService
  {
    private readonly RabbitMqConfiguration _rabbitMqConfiguration;
    private readonly EmailSendClient _emailClient;
    private readonly IAdvancedBus _advancedRabbitMqBus;
    private IDisposable _consumer;

    public EmailSenderService(AppSettingProvider appSettings, IBus rabbitMqBus)
    {
      _rabbitMqConfiguration = appSettings.RabbitMqConfiguration;
      _emailClient = new EmailSendClient(appSettings.SmtpConfiguration);
      _advancedRabbitMqBus = rabbitMqBus.Advanced;
    }

    public void Start()
    {
      var exchangeName = _rabbitMqConfiguration.ExchangeName;
      var queueName = _rabbitMqConfiguration.QueueName;
      var routingKey = _rabbitMqConfiguration.RoutingKey;

      var exchange = _advancedRabbitMqBus.ExchangeDeclare(exchangeName, ExchangeType.Topic);
      var queue = _advancedRabbitMqBus.QueueDeclare(queueName);

      _advancedRabbitMqBus.Bind(exchange, queue, routingKey);

      _consumer = _advancedRabbitMqBus.Consume<EmailMessage>(queue, (message, info) =>
      {
        Console.WriteLine(
          "Received EmailMessage. Id: {0}, Subject: {1}, Body: {2}, Recipient: {3}.",
          message.Body.Id,
          message.Body.Subject,
          message.Body.Body,
          message.Body.Recipient);

        try
        {
          Console.WriteLine("Sending e-mail message...");
          _emailClient.SendMessage(message.Body);
        }
        catch (Exception e)
        {
          Console.WriteLine("Failed to send e-mail message: " + e.Message);
          throw;
        }

        Console.WriteLine("Message has been sent successfully.");
      });

      Console.WriteLine("Waiting for message...");
    }

    public void Stop()
    {
      _consumer.Dispose();
    }
  }
}