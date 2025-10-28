using Domain.Entities.Gardens;

namespace Domain.Events.Gardens;

public class CompleteTreeRequestedEvent : BaseEvent
{
    public required Guid GardenId { get; set; }
    public required TreeState TreeState { get; set; }
}