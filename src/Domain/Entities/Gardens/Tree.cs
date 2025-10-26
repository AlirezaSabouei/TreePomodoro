using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Gardens;

[Owned]
public class Tree
{
    public TreeState TreeState { get; set; }
    public int Index { get; set; }
    public DateTime PlantedDate { get; set; }
    public int GrowthTimeInSeconds { get; set; } = 1500;
}