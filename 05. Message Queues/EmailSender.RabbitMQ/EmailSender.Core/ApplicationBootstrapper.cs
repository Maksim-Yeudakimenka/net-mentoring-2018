using EasyNetQ;

namespace EmailSender.Core
{
  public static class ApplicationBootstrapper
  {
    public static IBus ConfigureRabbitMqBus(RabbitMqConfiguration configuration)
    {
      var applicationName = configuration.ApplicationName;
      var host = configuration.Host;
      var vhost = configuration.VirtualHost;
      var port = configuration.Port;
      var user = configuration.User;
      var password = configuration.Password;

      return RabbitHutch.CreateBus(
        $"host={host}:{port};virtualHost={vhost};username={user};password={password};product={applicationName}");
    }
  }
}