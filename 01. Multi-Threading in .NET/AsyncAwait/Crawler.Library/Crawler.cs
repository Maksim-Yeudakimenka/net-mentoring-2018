using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawler.Library
{
  public class Crawler : IDisposable
  {
    private readonly HttpClient _client = new HttpClient();

    private readonly CrawlerOptions _crawlerOptions;
    private readonly ICollection<Uri> _visitedUrls = new List<Uri>();
    private readonly ICollection<Uri> _searchResults = new List<Uri>();

    public Crawler(CrawlerOptions options)
    {
      _crawlerOptions = options;
    }

    public async Task<IEnumerable<Uri>> BeginSearchAsync()
    {
      await RequestPageAsync(_crawlerOptions.SearchDepth, _crawlerOptions.StartUrl);

      return _searchResults;
    }

    private async Task RequestPageAsync(int depth, Uri url)
    {
      if (depth < 0)
      {
        return;
      }

      depth--;

      if (_visitedUrls.Contains(url))
      {
        return;
      }

      _visitedUrls.Add(url);

      var response = await _client.GetAsync(url);
      if (response.StatusCode == HttpStatusCode.NotFound)
      {
        return;
      }

      using (var htmlStream = await response.Content.ReadAsStreamAsync())
      {
        var html = new HtmlDocument();
        html.Load(htmlStream);

        SearchTerm(html, url);

        if (depth >= 0)
        {
          await ProcessHrefsAsync(depth, url, html);
        }
      }
    }

    private void SearchTerm(HtmlDocument html, Uri url)
    {
      var nodes = html.DocumentNode.SelectNodes("//*[contains(., '" + _crawlerOptions.SearchTerm + "')]");
      if (nodes != null && nodes.Count > 0)
      {
        _searchResults.Add(url);
      }
    }

    private async Task ProcessHrefsAsync(int depth, Uri url, HtmlDocument html)
    {
      var hrefs = html.DocumentNode.Descendants().Where(n => n.Name == "a" && n.Attributes.Any(a => a.Name == "href"));
      foreach (var link in hrefs)
      {
        var hrefValue = link.GetAttributeValue("href", string.Empty);
        if (hrefValue != string.Empty)
        {
          string refUrl;
          if (hrefValue.Contains("http"))
          {
            refUrl = hrefValue;
          }
          else
          {
            refUrl = url.Scheme + "://" + url.Host + hrefValue;
          }

          await RequestPageAsync(depth, new Uri(refUrl));
        }
      }
    }

    public void Dispose()
    {
      _client?.Dispose();
    }
  }
}