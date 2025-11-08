namespace Domain.Entities.Components;

public class Component : BaseEntity
{
    public string Name { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public string PreviewImageUrl { get; set; } = "";
}