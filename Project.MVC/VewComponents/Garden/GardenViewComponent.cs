using Microsoft.AspNetCore.Mvc;
using Project.Business.Services.Gardens;
using Project.MVC.Models;

namespace Project.MVC.VewComponents.Garden;

public class GardenViewComponent(GardenServices gardenServices) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(RequestType requestType)
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
        return View("Garden", gardenServices.Garden);
    }
}