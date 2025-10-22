using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Apis;

public class BaseApiController(IMapper mapper) : ControllerBase
{
    protected Guid PlayerId=>Guid.Parse(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value);
    
    protected readonly IMapper Mapper = mapper;
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}