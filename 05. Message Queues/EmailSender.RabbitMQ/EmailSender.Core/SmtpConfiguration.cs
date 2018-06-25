using System.Configuration;

namespace EmailSender.Core
{
  public class SmtpConfiguration
  {
    public SmtpConfiguration()
    {
      SmtpHost = ConfigurationManager.AppSettings["SmtpHost"];
      SmtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
      SmtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
      SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
    }

    public string SmtpHost { get; }
    public int SmtpPort { get; }
    public string SmtpUsername { get; }
    public string SmtpPassword { get; }
  }
}