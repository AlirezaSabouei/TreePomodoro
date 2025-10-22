namespace Project.Domain.Entities.Gardens;

public class Tree : BaseEntity
{
    public Garden Garden { get; set; }
    public TreeState TreeState { get; set; }
    public int Index { get; set; }
    public int GrowthTimeInSeconds { get; set; } = 1500;
}