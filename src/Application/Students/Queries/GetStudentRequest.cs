using Application.Common.Data;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Students.Queries;

public record GetStudentRequest : IRequest<Student?>
{
    public Guid StudentId { get; set; }
}

public class GetStudentRequestHandler(IContext context) : IRequestHandler<GetStudentRequest, Student?>
{
    public async Task<Student?> Handle(GetStudentRequest request, CancellationToken cancellationToken)
    {
        var student = await context.Students.FirstOrDefaultAsync(a=>a.Id == request.StudentId,cancellationToken);
        return student;
    }
}