using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Services.Gardens;
using Project.MVC.Models;

namespace Project.MVC.Controllers;

public class HomeController(GardenServices gardenServices, ILogger<HomeController> logger) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> RefreshGarden(RequestType requestType)
    {
        await gardenServices.GetOrCreateGardenAsync();
        switch (requestType)
        {
            case RequestType.PlantASeed:
                await gardenServices.PlantASeedAsync();
                break;
            case RequestType.KillTheSeed:
                await gardenServices.KillATreeAsync();
                break;
        }
        return PartialView("_garden", gardenServices.Garden);
    }
    
    [HttpGet]
    public async Task<IActionResult> LoadGarden(RequestType requestType)
    {
        return ViewComponent("Garden", new { requestType = requestType });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}