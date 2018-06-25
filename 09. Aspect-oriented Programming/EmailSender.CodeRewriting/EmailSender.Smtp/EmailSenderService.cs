using System;
using System.Linq;
using System.Timers;
using EmailSender.Core;
using EmailSender.DAL;
using Microsoft.Extensions.Logging;

namespace EmailSender.Smtp
{
  public class EmailSenderService
  {
    private readonly EmailMessageRepository _repository;
    private readonly EmailSendClient _emailClient;
    private readonly ILogger<EmailSenderService> _logger;
    private readonly Timer _timer;

    public EmailSenderService(AppSettingProvider appSettings, ConnectionStringProvider connectionStrings, ILoggerFactory loggerFactory)
    {
      _repository = new EmailMessageRepository(connectionStrings);
      _emailClient = new EmailSendClient(appSettings);
      _logger = loggerFactory.CreateLogger<EmailSenderService>();

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