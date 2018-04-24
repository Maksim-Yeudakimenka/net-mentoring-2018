using System;
using System.Linq;
using System.Timers;
using EmailSender.Core;
using EmailSender.DAL;

namespace EmailSender.Smtp
{
  public class EmailSenderService
  {
    private readonly EmailMessageRepository _repository;
    private readonly EmailSendClient _emailClient;
    private readonly Timer _timer;

    public EmailSenderService(AppSettingProvider appSettings, ConnectionStringProvider connectionStrings)
    {
      _repository = new EmailMessageRepository(connectionStrings);
      _emailClient = new EmailSendClient(appSettings);

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
        Console.WriteLine("Got {0} incompleted messages.", incompletedMessages.Count);

        foreach (var message in incompletedMessages)
        {
          _emailClient.SendMessage(message);
          _repository.SetCompleted(message);
          Console.WriteLine("Sent a message with subject {0} to {1}.", message.Subject, message.Recipient);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception was thrown in the main loop of the EmailSenderService: ");
        Console.WriteLine(ex.Message);
      }
      finally
      {
        _timer.Start();
      }
    }
  }
}