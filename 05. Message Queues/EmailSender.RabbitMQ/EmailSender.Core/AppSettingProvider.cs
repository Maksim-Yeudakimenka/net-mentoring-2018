namespace EmailSender.Core
{
  public class AppSettingProvider
  {
    public AppSettingProvider()
    {
      SmtpConfiguration = new SmtpConfiguration();
      RabbitMqConfiguration = new RabbitMqConfiguration();
    }

    public SmtpConfiguration SmtpConfiguration { get; }
    public RabbitMqConfiguration RabbitMqConfiguration { get; }
  }
}