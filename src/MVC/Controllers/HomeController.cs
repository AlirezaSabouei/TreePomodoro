using System.Diagnostics;
using Components.Models;
using Components.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult LoadGarden(RequestType requestType)
    {
        return ViewComponent(nameof(GardenViewComponent), new { requestType = requestType });
    }
    
    [HttpGet]
    public IActionResult LoadWater(RequestType requestType)
    {
        return ViewComponent(nameof(WaterViewComponent), new { requestType = requestType });
    }
    
    [HttpGet]
    public IActionResult LoadAlert()
    {
        return ViewComponent("AlertComponent");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
