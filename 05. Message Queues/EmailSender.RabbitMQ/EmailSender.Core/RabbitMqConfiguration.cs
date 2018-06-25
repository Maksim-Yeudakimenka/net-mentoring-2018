using System.Configuration;

namespace EmailSender.Core
{
  public class RabbitMqConfiguration
  {
    public RabbitMqConfiguration()
    {
      ApplicationName = ConfigurationManager.AppSettings["ApplicationName"];
      Host = ConfigurationManager.AppSettings["Host"];
      Port = ushort.Parse(ConfigurationManager.AppSettings["Port"]);
      User = ConfigurationManager.AppSettings["User"];
      Password = ConfigurationManager.AppSettings["Password"];
      VirtualHost = ConfigurationManager.AppSettings["VirtualHost"];
      QueueName = ConfigurationManager.AppSettings["QueueName"];
      ExchangeName = ConfigurationManager.AppSettings["ExchangeName"];
      RoutingKey = ConfigurationManager.AppSettings["RoutingKey"];
    }

    public string ApplicationName { get; }
    public string Host { get; }
    public ushort Port { get; }
    public string User { get; }
    public string Password { get; }
    public string VirtualHost { get; }
    public string QueueName { get; }
    public string ExchangeName { get; }
    public string RoutingKey { get; }
  }
}