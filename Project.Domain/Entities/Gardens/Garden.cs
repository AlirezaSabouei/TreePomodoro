namespace Project.Domain.Entities.Gardens;

public class Garden : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTimeOffset Date { get; set; }
    public List<Tree> Trees { get; set; } = [];

    public void RefreshTrees()
    {
        foreach (var tree in Trees)
        {
            if (!tree.Finished)
            {
                tree.Finished = (DateTimeOffset.UtcNow - tree.CreateDate).Minutes >= 25;
                if (tree.Finished)
                {
                    tree.TreeState = TreeState.Green;
                }
            }
        }
    }

    public void SpawnATree()
    {
        if (AnotherTreeIsGrowing())
        {
            throw new Exception("You cannot create another tree.");
        }

        if (Trees.Count == 25)
        {
            throw new Exception("Maximum trees for today is reached. Get some rest!");
        }

        var treeIndexes = Trees.Select(x => x.Index).ToList();

        var random = new Random();
        var randomIndex = random.Next(0,24);
        while (treeIndexes.Contains(randomIndex))
        {
            randomIndex = random.Next(0,24);
        }
        Trees.Add(new Tree
        {
            Index = randomIndex,
            TreeState = TreeState.Growing,
            Finished = false,
            Garden = this
        });
    }

    private bool AnotherTreeIsGrowing()
    {
        return Trees.Any(a => a.TreeState == TreeState.Growing);
    }
}