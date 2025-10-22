using Domain.Events;
using MediatR;

namespace Application.Students.EventHandlers;

public class StudentCreatedEventHandler : INotificationHandler<StudentCreatedEvent>
{
    public Task Handle(StudentCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine(notification.Student.Id);
        //Do something
        return Task.CompletedTask;
    }
}