using System.Net;
using System.Net.Mail;
using EmailSender.Core;
using EmailSender.DAL;

namespace EmailSender.Smtp
{
  public class EmailSendClient
  {
    private readonly AppSettingProvider _appSettings;

    public EmailSendClient(AppSettingProvider appSettings)
    {
      _appSettings = appSettings;
    }

    public void SendMessage(EmailMessage message)
    {
      using (var mail = new MailMessage(_appSettings.SmtpUsername, message.Recipient, message.Subject, message.Body))
      {
        using (var smtpClient = new SmtpClient(_appSettings.SmtpHost, _appSettings.SmtpPort))
        {
          smtpClient.EnableSsl = true;
          smtpClient.UseDefaultCredentials = false;
          smtpClient.Credentials = new NetworkCredential(_appSettings.SmtpUsername, _appSettings.SmtpPassword);
          smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

          smtpClient.Send(mail);
        }
      }
    }
  }
}