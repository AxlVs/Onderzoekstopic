using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ODT.Controllers
{
  public class ShopController : Controller
  {
    
    private readonly IStringLocalizer<ShopController> _localizer;

    public ShopController(IStringLocalizer<ShopController> localizer)
    {
      _localizer = localizer;
    }

    public IActionResult Index()
    {
      ViewData["controllerTitle"] = _localizer["winkelTitel"];
      return View();
    }
  }
}