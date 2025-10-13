using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Business.Services;
using Project.Domain.Entities;

namespace Project.RazorPages.API;

[Route("api/[controller]")]
[ApiController]
public class StudentsController(StudentServices studentServices) : ControllerBase
{
    private readonly StudentServices _studentServices = studentServices;

    [HttpGet]
    public async Task<IActionResult> Get(string name)
    {
        var students = await _studentServices.GetStudentsByName(name);
        return Ok(students);
    }
}
