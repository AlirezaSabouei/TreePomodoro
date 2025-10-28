using System.ComponentModel.DataAnnotations.Schema;
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

    [NotMapped]
    public GardenState GardenState
    {
        get
        {
            if (Trees.Count == 25) return GardenState.Full;
            if (Trees.Any(a => a.TreeState == TreeState.Seed)) return GardenState.GrowingATree;
            return GardenState.ReadyToPlantASeed;
        }
    }

    [NotMapped]
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
}