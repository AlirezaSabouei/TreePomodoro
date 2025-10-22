using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class StudentController : Controller
{
    // GET
    public IActionResult Index()
    {
        var student = new Student
        {
            Id = Guid.NewGuid(),
            Name = "John Doe"
        };
        return View(student);
    }
}