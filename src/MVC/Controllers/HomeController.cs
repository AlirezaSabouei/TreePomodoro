using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Views.Shared.Components.WaterComponent;

namespace MVC.Controllers;

public class HomeController : Controller
{
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    // public async Task<IActionResult> RefreshGarden(RequestType requestType)
    // {
    //     
    //     await createGardenCommandHandler.Handle();
    //     switch (requestType)
    //     {
    //         case RequestType.PlantASeed:
    //             await gardenServices.PlantASeedAsync();
    //             break;
    //         case RequestType.KillTheSeed:
    //             await gardenServices.KillATreeAsync();
    //             break;
    //     }
    //     return PartialView("_garden", _garden);
    // }
    
    [HttpGet]
    public IActionResult LoadGarden(RequestType requestType)
    {
        return ViewComponent("GardenComponent", new { requestType = requestType });
    }
    
    [HttpGet]
    public IActionResult LoadWater()
    {
        return ViewComponent(nameof(WaterComponent));
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
