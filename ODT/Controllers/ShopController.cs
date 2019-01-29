using Microsoft.AspNetCore.Mvc;

namespace ODT.Controllers
{
  public class ShopController : Controller
  {
    // GET
    public IActionResult Index()
    {
      return View();
    }
  }
}