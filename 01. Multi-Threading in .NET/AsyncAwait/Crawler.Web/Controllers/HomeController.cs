using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Crawler.Library;
using Microsoft.AspNetCore.Mvc;
using Crawler.Web.Models;

namespace Crawler.Web.Controllers
{
  public class HomeController : Controller
  {
    public async Task<IActionResult> Index()
    {
      var model = new CrawlerOptionsViewModel
      {
        StartUrl = "https://habrahabr.ru/post/330618/",
        SearchDepth = 1,
        SearchTerm = "ssis"
      };

      return View(model);
    }

    public async Task<IActionResult> Search(CrawlerOptionsViewModel model)
    {
      IEnumerable<Uri> results;

      var options = new CrawlerOptions
      {
        StartUrl = new Uri(model.StartUrl),
        SearchDepth = model.SearchDepth,
        SearchTerm = model.SearchTerm
      };

      using (var termFinder = new Library.Crawler(options))
      {
        results = await termFinder.BeginSearchAsync();
      }

      return Json(results);
    }

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}