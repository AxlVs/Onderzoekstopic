using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ODT.Controllers
{
  public class ShopController : Controller
  {
    
    private readonly IStringLocalizer<HomeController> _localizer;

    public ShopController(IStringLocalizer<HomeController> localizer)
    {
      _localizer = localizer;
    }

    public IActionResult Index()
    {
      ViewData["myTitle"] = _localizer["winkelTitel"];
      return View();
    }
  }
}