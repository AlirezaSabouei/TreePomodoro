using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities.Gardens;

public class Garden : BaseEntity
{
    public Guid UserId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public List<Tree> Trees { get; set; } = [];

    [NotMapped]
    public GardenState GardenState
    {
        get
        {
            RefreshGarden();
            if (Trees.Count == 25)
            {
                return GardenState.Full;
            }
            if (Trees.All(a => a.TreeState != TreeState.Seed))
            {
                return GardenState.ReadyToPlantASeed;
            }
            return GardenState.GrowingATree;
        }
    }

    [NotMapped]
    public int RemainigSeconds
    {
        get
        {
            RefreshGarden();
            var seed = Trees.FirstOrDefault(a => a.TreeState == TreeState.Seed);
            if (seed == null)
            {
                return 0;
            }
            var elapsedSeconds = (int)(DateTime.Now - seed.CreateDate).TotalSeconds;
            var remainingSeconds = 1500 - elapsedSeconds; // 25 minutes = 1500 seconds
            return remainingSeconds > 0 ? remainingSeconds : 0;
        }
    }


    public void RefreshGarden()
    {
        var seed = Trees.FirstOrDefault(a => a.TreeState == TreeState.Seed);
        if (seed == null)
        {
            return;
        }
        var finished = (DateTime.Now - seed.CreateDate).Minutes >= 25;
        if (finished)
        {
            seed.TreeState = TreeState.Green;
        }
    }

    public Tree? AddASeed()
    {
        ValidateGardenHasNoMoreThan25Trees();

        var seed = Trees.FirstOrDefault(a => a.TreeState == TreeState.Seed);
        if (seed == null)
        {
            seed = CreateANewSeed();
            Trees.Add(seed);
            return seed;
        }

        return null;
    }

    private void ValidateGardenHasNoMoreThan25Trees()
    {
        if (Trees.All(a => a.TreeState is TreeState.Green or TreeState.Dry) && Trees.Count == 25)
        {
            throw new Exception("You garden is full for today! Get some rest!");
        }
    }

    private Tree CreateANewSeed()
    {
        Tree seed;
        seed = new Tree
        {
            Index = CreateARandomIndexForTheTree(),
            TreeState = TreeState.Seed,
            Garden = this
        };

        return seed;
    }

    private int CreateARandomIndexForTheTree()
    {
        var treeIndexes = Trees.Select(x => x.Index).ToList();

        var random = new Random();
        var randomIndex = random.Next(0, 25);
        while (treeIndexes.Contains(randomIndex))
        {
            randomIndex = random.Next(0, 25);
        }

        return randomIndex;
    }
}