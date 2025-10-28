namespace Domain.Events.Gardens;

public class GardenUpdatedEvent : BaseEvent
{
    public Guid GardenId { get; set; }
    public List<ChangedProperty> ChangedProperties { get; set; } = [];
}