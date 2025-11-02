using Microsoft.AspNetCore.Mvc;

namespace MVC.Views.Shared.Components.WaterComponent;

public class WaterComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {

        return View("WaterComponentView");
    }
}