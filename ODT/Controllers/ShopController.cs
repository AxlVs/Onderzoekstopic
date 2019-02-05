using System.Collections.Generic;
using System.Linq;
using DAL.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace ODT.Controllers
{
  public class ShopController : Controller
  {
    private VlaggenRepository repo = new VlaggenRepository();
    
    // localization
    private readonly IStringLocalizer<ShopController> _localizer;
    
    // caching
    private IMemoryCache _cache;

    public ShopController(IStringLocalizer<ShopController> localizer, IMemoryCache memoryCache)
    {
      _localizer = localizer;
      _cache = memoryCache;
    }

    public IActionResult Index()
    {
      // return correct language title
      ViewData["controllerTitle"] = _localizer["winkelTitel"];
      
      // return data
      List<Vlag> vlaggen = repo.ReadVlaggen().ToList();
      
      // Caching
      /*
      var cacheEntry = _cache.GetOrCreate(CacheKeys.Entry, entry =>
      {
        entry.SlidingExpiration = TimeSpan.FromDays(5);
        return DateTime.Now;
      });

      ViewBag.Cache = cacheEntry;
    */
      return View(vlaggen);
    }
  }
}