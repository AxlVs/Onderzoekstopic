using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using Domain;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace ODT.Controllers
{
  
//  [ResponseCache(Duration = 60)]
  public class ShopController : Controller
  {
    private VlaggenRepository repo = new VlaggenRepository();
    
    // localization
    private readonly IStringLocalizer<ShopController> _localizer;
    
    // caching
    //private IMemoryCache _cache;
    private IDistributedCache _distributedCache;
    
    public ShopController(IStringLocalizer<ShopController> localizer, IDistributedCache memoryCache)
    {
      _localizer = localizer;
      _distributedCache = memoryCache;
    }
    
    public async Task<IActionResult> Index()
    {
      const string key = "cachedTime";
      const string message = "hello";

      await _distributedCache.SetStringAsync(key, message);
      var cachedMessage = await _distributedCache.GetStringAsync(key);

      ViewBag.distributedCacheValue = cachedMessage;
      
      // return correct language title
      ViewData["controllerTitle"] = _localizer["winkelTitel"];
      
      // return data
//      List<Vlag> vlaggen = repo.ReadVlaggen().ToList();

      ViewBag.cacheTime = DateTime.Now.ToLongTimeString();
      
      // Test json
      if (_distributedCache.GetAsync("vlaggenJson") == null)
      {
        List<Vlag> vlaggen = repo.ReadVlaggen().ToList();
        String json = JsonConvert.SerializeObject(vlaggen);
        //TODO Wegschrijven
      }
      
      return View(vlaggen);
    }
  }
}