using System;

namespace Crawler.Library
{
  public class CrawlerOptions
  {
    public Uri StartUrl { get; set; }

    public int SearchDepth { get; set; }

    public string SearchTerm { get; set; }
  }
}