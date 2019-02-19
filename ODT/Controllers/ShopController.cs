using System;
using System.Collections.Generic;
using System.Linq;
using DAL.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace ODT.Controllers
{
  
  [ResponseCache(Duration = 60)]
  public class ShopController : Controller
  {
    private VlaggenRepository repo = new VlaggenRepository();
    
    // localization
    private readonly IStringLocalizer<ShopController> _localizer;
    
    // caching
    //private IMemoryCache _cache;
    //private IDistributedCache _cache;
    
    public ShopController(IStringLocalizer<ShopController> localizer, IDistributedCache memoryCache)
    {
      _localizer = localizer;
      //_cache = memoryCache;
    }
    
    public IActionResult Index()
    {
      // return correct language title
      ViewData["controllerTitle"] = _localizer["winkelTitel"];
      
      // return data
      List<Vlag> vlaggen = repo.ReadVlaggen().ToList();

      ViewBag.cacheTime = DateTime.Now.ToLongTimeString();
     
      return View(vlaggen);
    }
  }
}