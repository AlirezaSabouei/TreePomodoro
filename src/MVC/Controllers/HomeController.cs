using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Views.Shared.Components.GardenComponent;
using MVC.Views.Shared.Components.WaterComponent;

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
        return ViewComponent(nameof(GardenComponent), new { requestType = requestType });
    }
    
    [HttpGet]
    public IActionResult LoadWater(RequestType requestType)
    {
        return ViewComponent(nameof(WaterComponent), new { requestType = requestType });
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
