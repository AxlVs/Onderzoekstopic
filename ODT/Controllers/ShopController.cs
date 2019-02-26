using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace ODT.Controllers
{
  
  [ResponseCache(CacheProfileName = "DefaultProfile")]
  public class ShopController : Controller
  {
    private VlaggenRepository repo = new VlaggenRepository();
    
    // localization
    private readonly IStringLocalizer<ShopController> _localizer;
    
    // caching
    //private IMemoryCache _cache;
    private IDistributedCache _distributedCache;
    
    public ShopController(IStringLocalizer<ShopController> localizer,
      IDistributedCache memoryCache)
    {
      _localizer = localizer;
      _distributedCache = memoryCache;
    }
    
    public async Task<IActionResult> Index()
    { 
      // De titel via de controller meegeven, gebruik makend van de localizer
      //  --> Zorgt ervoor dat naar een vertaling wordt gezocht in de resource file van de controller ipv de view
      ViewData["controllerTitle"] = _localizer["winkelTitel"];

      ViewBag.cacheTime = DateTime.Now.ToLongTimeString();
      
      // Distributed Caching met Redis
      var cachedData = await _distributedCache.GetStringAsync("vlaggenJson");
      List<Vlag> vlaggen;
      
      if (string.IsNullOrEmpty(cachedData))
      {
        vlaggen = repo.ReadVlaggen().ToList();
        string json = JsonConvert.SerializeObject(vlaggen);

        var options = new DistributedCacheEntryOptions()
          .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
        
        await _distributedCache.SetStringAsync("vlaggenJson", json, options);
      }
      else
      {
        vlaggen = JsonConvert.DeserializeObject<List<Vlag>>(cachedData);
      }
      
      return View(vlaggen);
    }
  }
}