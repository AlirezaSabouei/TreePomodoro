namespace Project.Domain.Entities.Gardens;

public class Tree : BaseEntity
{
    public Garden Garden { get; set; }
    public TreeState TreeState { get; set; }
    public bool Finished { get; set; } = false;
    public int Index { get; set; }
}