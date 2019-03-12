using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using IFormFile = Microsoft.AspNetCore.Http.IFormFile;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ODT.Controllers
{
  public class ShopController : Controller
  {
    private IVlaggenManager mgr = new VlaggenManager();
    
    // localization
    private readonly IStringLocalizer<ShopController> _localizer;
    
    // caching
    //private IMemoryCache _cache;
    private IDistributedCache _distributedCache;
    
    private IMemoryCache _memoryCache;
    
    // Image uploaden
    private readonly IHostingEnvironment he;
    
    public ShopController(IStringLocalizer<ShopController> localizer,
      IDistributedCache distributedCache, IMemoryCache memoryCache,
      Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
      _localizer = localizer;
      _distributedCache = distributedCache;
      _memoryCache = memoryCache;
      he = hostingEnvironment;
    }
    
//    [ResponseCache(CacheProfileName = "DefaultProfile")]
    public async Task<IActionResult> Index()
    { 
      // De titel via de controller meegeven, gebruik makend van de localizer
      //  --> Zorgt ervoor dat naar een vertaling wordt gezocht in de resource file van de controller ipv de view
      ViewData["controllerTitle"] = _localizer["winkelTitel"];

      ViewBag.cacheTime = DateTime.Now.ToLongTimeString();
      
      // In-Memory Caching
      DateTime currentTime;
      
      bool isExist = _memoryCache.TryGetValue("CacheTime", out currentTime);  
      if (!isExist)  
      {
        currentTime = DateTime.Now;                  
        var cacheEntryOptions = new MemoryCacheEntryOptions()  
          .SetSlidingExpiration(TimeSpan.FromSeconds(30));

        _memoryCache.Set("CacheTime", currentTime, cacheEntryOptions);
      }

      ViewBag.memoryCacheTime = currentTime;
      
      // Distributed Caching met Redis
      var cachedData = await _distributedCache.GetStringAsync("vlaggenJson");
      List<Vlag> vlaggen;
      
      if (string.IsNullOrEmpty(cachedData))
      {
        vlaggen = mgr.GetVlaggen().ToList();
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

    [ResponseCache(CacheProfileName = "DefaultProfile", VaryByQueryKeys = new[] {"id"})]
    public IActionResult Flag(int id)
    {
      ViewData["controllerTitle"] = _localizer["winkelTitel"];
      ViewBag.cacheTime = DateTime.Now.ToLongDateString();
      
      Vlag v = mgr.GetVlag(id);
      return View(v);
    }

    [HttpGet]
    public IActionResult Add()
    {
      ViewData["controllerTitle"] = _localizer["winkelTitel"];
      return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm]Vlag vlag, [FromForm] IFormFile image)
    {
      Vlag toAddVlag = vlag;
      if (image != null)
      {
        image.CopyTo(new FileStream("wwwroot/images/vlag/" + image.FileName, FileMode.Create));

        toAddVlag.Afbeelding = "/images/vlag/" + image.FileName;
      }

      mgr.AddVlag(toAddVlag);
      return RedirectToAction("Index");
    }
  }
}