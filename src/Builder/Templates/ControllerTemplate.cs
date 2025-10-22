using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.Models;
using Project.Business.Services.{{EntityPluralName}};

namespace Project.MVC.Controllers;

public class {{EntityName}}Controller(
    {{EntityName}}Services service,
    ILogger<{{EntityName}}Controller> logger) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
