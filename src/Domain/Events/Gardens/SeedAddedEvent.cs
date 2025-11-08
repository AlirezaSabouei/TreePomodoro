namespace Domain.Events.Gardens;

public class SeedAddedEvent : BaseEvent
{
    public required Guid GardenId { get; set; }
    public required int GrowthTimeInSeconds { get; set; }
    public required Guid UserId { get; set; }
}