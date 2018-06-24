using System;
using System.Linq;
using System.Timers;
using Castle.DynamicProxy;
using EmailSender.Core;
using EmailSender.DAL;
using EmailSender.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace EmailSender.Smtp
{
  public class EmailSenderService
  {
    private readonly IEmailMessageRepository _repository;
    private readonly IEmailSendClient _emailClient;
    private readonly ILogger<EmailSenderService> _logger;
    private readonly Timer _timer;

    public EmailSenderService(AppSettingProvider appSettings, ConnectionStringProvider connectionStrings, ILoggerFactory loggerFactory)
    {
      _logger = loggerFactory.CreateLogger<EmailSenderService>();

      var proxyGenerator = new ProxyGenerator();

      _repository = proxyGenerator.CreateInterfaceProxyWithTarget<IEmailMessageRepository>(
        new EmailMessageRepository(connectionStrings),
        new LoggingInterceptor(_logger));

      _emailClient = proxyGenerator.CreateInterfaceProxyWithTarget<IEmailSendClient>(
        new EmailSendClient(appSettings),
        new LoggingInterceptor(loggerFactory.CreateLogger<EmailSendClient>()));

      _timer = new Timer(appSettings.RepeatInterval)
      {
        AutoReset = true
      };

      _timer.Elapsed += this.MainLoop;
    }

    public void Start()
    {
      _timer.Start();
    }

    public void Stop()
    {
      _timer.Stop();
    }

    private void MainLoop(object sender, ElapsedEventArgs e)
    {
      _timer.Stop();

      try
      {
        var incompletedMessages = _repository.GetAllIncompleted().ToList();
        _logger.LogInformation("Got {IncompletedMessageCount} incompleted message(s).", incompletedMessages.Count);

        foreach (var message in incompletedMessages)
        {
          _emailClient.SendMessage(message);
          _repository.SetCompleted(message);

          _logger.LogInformation(
            "Sent a message with subject {MessageSubject} to {MessageRecipient}.",
            message.Subject,
            message.Recipient);
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Exception was thrown in the main loop of the EmailSenderService.");
      }
      finally
      {
        _timer.Start();
      }
    }
  }
}