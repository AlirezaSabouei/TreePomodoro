using System.Diagnostics;
using Application.Gardens.Commands;
using Domain.Entities.Gardens;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController(
    IRequestHandler<CreateGardenCommand, Garden> createGardenCommandHandler,
    IRequestHandler<CreateTreeCommand, Garden> createTreeCommandHandler,
    IRequestHandler<KillTreeCommand, Garden> killTreeCommandHandler,
    ILogger<HomeController> logger) : Controller
{
    private Garden _garden = new() ;
    
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
