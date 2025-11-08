namespace Domain.Events.Gardens;

public class SeedKilledEvent : BaseEvent
{
    public required Guid GardenId { get; set; }
}