using System.Configuration;

namespace EmailSender.Core
{
  public class ConnectionStringProvider
  {
    public string EmailMessaging => ConfigurationManager.ConnectionStrings["emailMessaging"].ConnectionString;
  }
}