using EmailSender.DAL;

namespace EmailSender.Smtp
{
  public interface IEmailSendClient
  {
    void SendMessage(EmailMessage message);
  }
}