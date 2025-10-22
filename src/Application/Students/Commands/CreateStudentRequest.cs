using Application.Common.Data;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Students.Commands;

public record CreateStudentRequest : IRequest<Student>
{
    public required Student Student { get; set; }
}

public class CreateStudentRequestHandler(IContext context) : IRequestHandler<CreateStudentRequest, Student>
{
    public async Task<Student> Handle(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        request.Student.AddDomainEvent(new StudentCreatedEvent(request.Student));
        await context.Students.AddAsync(request.Student, cancellationToken);
        await context.SaveChangesAsync();
        return request.Student;
    }
}