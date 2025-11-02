using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Glasses;

public class Glass : BaseEntity
{
    [BsonRepresentation(BsonType.String)] // store GUID as string
    public Guid UserId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
}