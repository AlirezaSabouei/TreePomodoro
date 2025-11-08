using System.ComponentModel.DataAnnotations.Schema;
using Domain.Events.Gardens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Gardens;

public class Garden : BaseEntity
{
    [BsonRepresentation(BsonType.String)] // store GUID as string
    public Guid UserId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public List<Tree> Trees { get; set; } = [];

    [BsonIgnore]
    public GardenState GardenState
    {
        get
        {
            if (Trees.Count == 25) return GardenState.Full;
            if (Trees.Any(a => a.TreeState == TreeState.Seed)) return GardenState.GrowingATree;
            return GardenState.ReadyToPlantASeed;
        }
    }

    [BsonIgnore]
    public int RemainingSeconds
    {
        get
        {
            if (GardenState != GardenState.GrowingATree)
            {
                return 0;
            }
            var tree = Trees.First(a => a.TreeState == TreeState.Seed);
            return (int)(tree.GrowthTimeInSeconds - (DateTime.Now - tree.PlantedDate).TotalSeconds);
        }
    }

    public void AddSeed(int growthTimeInSeconds)
    {
        ValidateIfGardenHasSpaceForNewTree();
        var seed = Trees.FirstOrDefault(a => a.TreeState == TreeState.Seed);
        if (seed == null)
        {
            seed = CreateANewSeed(growthTimeInSeconds);
            Trees.Add(seed);
            CreateSeedAddedEvent(growthTimeInSeconds);
        }
        else
        {
            // TODO: Exception
        }
    }

    private void ValidateIfGardenHasSpaceForNewTree()
    {
        if (Trees.Count == 25 && Trees.All(a => a.TreeState is TreeState.Green or TreeState.Dry))
        {
            throw new Exception("Your garden is full for today! Get some rest!");
        }
    }

    private Tree CreateANewSeed(int growthTimeInSeconds)
    {
        var seed = new Tree
        {
            Index = CreateARandomIndexForTheTree(),
            TreeState = TreeState.Seed,
            PlantedDate = DateTime.Now,
            GrowthTimeInSeconds = growthTimeInSeconds
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

    private void CreateSeedAddedEvent(int growthTimeInSeconds)
    {
        var seedAddedEvent = new SeedAddedEvent()
        {
            GardenId = Id,
            GrowthTimeInSeconds = growthTimeInSeconds,
            UserId = UserId
        };
        AddDomainEvent(seedAddedEvent);
    }

    public void KillSeed()
    {
        var seed = Trees.FirstOrDefault(a=>a.TreeState == TreeState.Seed);
        if (seed == null)
        {
            //TODO : CUstom Exception
            throw new Exception("No seed!");
        }
        Trees.First(a => a.TreeState == TreeState.Seed).TreeState = TreeState.Dry;
        CreateSeedKilledEvent();
    }

    private void CreateSeedKilledEvent()
    {
        var seedKilledEvent = new SeedKilledEvent()
        {
            GardenId = Id
        };
        AddDomainEvent(seedKilledEvent);
    }

    public void CompleteSeed()
    {
        var seed = Trees.FirstOrDefault(a=>a.TreeState == TreeState.Seed);
        if (seed == null)
        {
            //TODO : CUstom Exception
            throw new Exception("No seed!");
        }
        Trees.First(a => a.TreeState == TreeState.Seed).TreeState = TreeState.Green;
        CreateSeedCompletedEvent();
    }
    
    private void CreateSeedCompletedEvent()
    {
        var seedCompletedEvent = new SeedCompletedEvent()
        {
            UserId = UserId,
            GardenId = Id
        };
        AddDomainEvent(seedCompletedEvent);
    }
}