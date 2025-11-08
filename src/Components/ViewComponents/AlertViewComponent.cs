using Microsoft.AspNetCore.Mvc;

namespace Components.ViewComponents;

public class AlertViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {

        return View("Index","Hello World");
    }
}