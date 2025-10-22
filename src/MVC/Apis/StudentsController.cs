using Application.Students.Queries;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Apis;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "SimplePlayer,SuperAdmin,Admin")]
public class StudentsController(IMapper mapper) : BaseApiController(mapper)
{
    [HttpGet]
    public async Task<Student?> GetStudents()
    {
        try
        {
            var request = new GetStudentRequest()
            {
                StudentId = Guid.NewGuid()
            };
            
            var result=await Mediator.Send(request);
            return result;
            // return Mapper.Map<List<FactionDto>>(result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            foreach (var error in ex.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            //return BadRequest(ModelState);
        }
        catch (Exception e)
        {
            //return BadRequest(e.Message);
        }

        return null;
    }
}