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
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentServices.GetAll().ToListAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var students = await _studentServices.GetByIdAsync(id);
        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Student student)
    {
        var students = await _studentServices.CreateAsync(student);
        return Ok(students);
    }
}
