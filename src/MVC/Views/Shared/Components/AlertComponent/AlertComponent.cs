using Microsoft.AspNetCore.Mvc;

namespace MVC.Views.Shared.Components.AlertComponent;

public class AlertComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {

        return View("AlertComponentView");
    }
}