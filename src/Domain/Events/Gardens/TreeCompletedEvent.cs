using Domain.Entities.Gardens;
using MediatR;

namespace Domain.Events.Gardens;

public class TreeCompletedEvent : BaseEvent
{
    public required Guid GardenId { get; set; }
    public required TreeState TreeState { get; set; }
}