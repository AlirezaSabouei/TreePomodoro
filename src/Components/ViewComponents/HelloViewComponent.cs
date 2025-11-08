using Domain.Entities.Components;
using Microsoft.AspNetCore.Mvc;

namespace Components.ViewComponents;

public class HelloViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string name)
    {
        var message = $"Hello, {name}!";
        return View("Index", message);
    }

    public Component GetDescriptor() => new()
    {
        Name = "Hello",
        Title = "Hello Message",
        Description = "Displays a friendly greeting.",
        Category = "General"
    };
}