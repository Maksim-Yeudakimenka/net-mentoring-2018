using System.Net;
using System.Net.Mail;
using EmailSender.CommonTypes;
using EmailSender.Core;

namespace EmailSender.Smtp
{
  public class EmailSendClient
  {
    private readonly SmtpConfiguration _configuration;

    public EmailSendClient(SmtpConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void SendMessage(EmailMessage message)
    {
      using (var mail = new MailMessage(_configuration.SmtpUsername, message.Recipient, message.Subject, message.Body))
      {
        using (var smtpClient = new SmtpClient(_configuration.SmtpHost, _configuration.SmtpPort))
        {
          smtpClient.EnableSsl = true;
          smtpClient.UseDefaultCredentials = false;
          smtpClient.Credentials = new NetworkCredential(_configuration.SmtpUsername, _configuration.SmtpPassword);
          smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

          smtpClient.Send(mail);
        }
      }
    }
  }
}