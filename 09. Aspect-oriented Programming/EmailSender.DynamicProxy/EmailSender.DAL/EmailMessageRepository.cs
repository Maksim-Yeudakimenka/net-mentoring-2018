using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using EmailSender.Core;

namespace EmailSender.DAL
{
  public class EmailMessageRepository
  {
    private readonly string _connectionString;

    public EmailMessageRepository(ConnectionStringProvider connectionStrings)
    {
      _connectionString = connectionStrings.EmailMessaging;
    }

    public IEnumerable<EmailMessage> GetAllIncompleted()
    {
      var sql = "SELECT * FROM Email_Message WHERE Completed = 0";

      using (var db = new SqlConnection(_connectionString))
      {
        return db.Query<EmailMessage>(sql);
      }
    }

    public void SetCompleted(EmailMessage message)
    {
      var sql =
        "UPDATE Email_Message " +
        "SET Completed = 1 " +
        "WHERE Id = @Id";

      using (var db = new SqlConnection(_connectionString))
      {
        db.Execute(sql, message);
      }
    }
  }
}