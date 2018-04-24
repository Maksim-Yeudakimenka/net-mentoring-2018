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

      HostFactory.Run(serviceConfig =>
      {
        serviceConfig.Service<EmailSenderService>(x =>
        {
          x.ConstructUsing(() => new EmailSenderService(appSettings, connectionStrings));
          x.WhenStarted(svc => svc.Start());
          x.WhenStopped(svc => svc.Stop());
        });
      });
    }
  }
}
