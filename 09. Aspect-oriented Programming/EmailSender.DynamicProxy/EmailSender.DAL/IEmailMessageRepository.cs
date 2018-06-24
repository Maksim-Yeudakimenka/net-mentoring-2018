using System.Collections.Generic;

namespace EmailSender.DAL
{
  public interface IEmailMessageRepository
  {
    IEnumerable<EmailMessage> GetAllIncompleted();
    void SetCompleted(EmailMessage message);
  }
}