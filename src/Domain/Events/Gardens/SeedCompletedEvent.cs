namespace Domain.Events.Gardens;

public class SeedCompletedEvent : BaseEvent
{
    public required Guid UserId { get; set; }
    public required Guid GardenId { get; set; }
}