using System.Diagnostics;
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
        return ViewComponent("Garden", new { requestType = requestType });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
