using Domain.Entities;

namespace Domain.Events;

public class StudentCreatedEvent(Student student) : BaseEvent
{
    public Student Student { get; set; } = student;
}