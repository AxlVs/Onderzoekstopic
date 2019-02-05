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
      
      return View(vlaggen);
    }
  }
}