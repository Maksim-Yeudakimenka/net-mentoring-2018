namespace Crawler.Web.Models
{
  public class CrawlerOptionsViewModel
  {
    public string StartUrl { get; set; }

    public int SearchDepth { get; set; }

    public string SearchTerm { get; set; }
  }
}