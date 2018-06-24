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
      var connectionStrings = new ConnectionStringProvider();

      var applicationConfiguration = ServiceBootstrapper.ConfigureApplicationConfiguration();
      var loggerFactory = ServiceBootstrapper.ConfigureLogging(applicationConfiguration);

      HostFactory.Run(serviceConfig =>
      {
        serviceConfig.Service<EmailSenderService>(x =>
        {
          x.ConstructUsing(() => new EmailSenderService(appSettings, connectionStrings, loggerFactory));
          x.WhenStarted(svc => svc.Start());
          x.WhenStopped(svc => svc.Stop());
        });
      });
    }
  }
}
