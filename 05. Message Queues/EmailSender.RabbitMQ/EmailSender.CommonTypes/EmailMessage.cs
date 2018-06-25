namespace EmailSender.CommonTypes
{
  public class EmailMessage
  {
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string Recipient { get; set; }
  }
}