using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]  // store GUID as string
    public Guid Id { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public DateTimeOffset UpdateDate { get; set; }
}
