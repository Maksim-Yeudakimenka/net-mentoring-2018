using EmailSender.Core;
using EmailSender.Smtp;
using Topshelf;

namespace EmailSender.WinSvc
{
  class Program
  {
    static void Main(string[] args)
    {
      var appSettings = new AppSettingProvider();
      var rabbitMqBus = ApplicationBootstrapper.ConfigureRabbitMqBus(appSettings.RabbitMqConfiguration);

      HostFactory.Run(serviceConfig =>
      {
        serviceConfig.Service<EmailSenderService>(x =>
        {
          x.ConstructUsing(() => new EmailSenderService(appSettings, rabbitMqBus));
          x.WhenStarted(svc => svc.Start());
          x.WhenStopped(svc => svc.Stop());
        });
      });
    }
  }
}
