using System.Collections.Generic;
using System.Linq;
using DAL.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ODT.Models;

namespace ODT.Controllers
{
  public class ShopController : Controller
  {
    private VlaggenRepository repo = new VlaggenRepository();
    private readonly IStringLocalizer<ShopController> _localizer;

    public ShopController(IStringLocalizer<ShopController> localizer)
    {
      _localizer = localizer;
    }

    public IActionResult Index()
    {
      // return correct language title
      ViewData["controllerTitle"] = _localizer["winkelTitel"];
      
      // return data
      List<Vlag> vlaggen = repo.ReadVlaggen().ToList();
      
      // Caching
      
      ObjectCache cache = MemoryCache.Default;  
      string fileContents = cache["filecontents"] as string;  

      if (fileContents == null)  
      {  
        CacheItemPolicy policy = new CacheItemPolicy();  

        List<string> filePaths = new List<string>();  
        filePaths.Add("c:\\cache\\example.txt");  

        policy.ChangeMonitors.Add(new   
          HostFileChangeMonitor(filePaths));  

        // Fetch the file contents.  
        fileContents =   
          File.ReadAllText("c:\\cache\\example.txt");  

        cache.Set("filecontents", fileContents, policy);  
      }  

      Label1.Text = fileContents;  
    }  
      
      return View(vlaggen);
    }
  }
}