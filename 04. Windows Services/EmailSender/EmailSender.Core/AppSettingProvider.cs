using System.Configuration;

namespace EmailSender.Core
{
  public class AppSettingProvider
  {
    public int RepeatInterval => int.Parse(ConfigurationManager.AppSettings["RepeatInterval"]);
    public string SmtpHost => ConfigurationManager.AppSettings["SmtpHost"];
    public int SmtpPort => int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
    public string SmtpUsername => ConfigurationManager.AppSettings["SmtpUsername"];
    public string SmtpPassword => ConfigurationManager.AppSettings["SmtpPassword"];
  }
}