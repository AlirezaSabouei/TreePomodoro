using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Common.Data;

public class LocalDateTimeSerializer : SerializerBase<DateTime>
{
    public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var utcDateTime = context.Reader.ReadDateTime();
        var dateTime = BsonUtils.ToDateTimeFromMillisecondsSinceEpoch(utcDateTime);
        return dateTime.ToLocalTime();
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
    {
        var utcDateTime = value.ToUniversalTime();
        var milliseconds = BsonUtils.ToMillisecondsSinceEpoch(utcDateTime);
        context.Writer.WriteDateTime(milliseconds);
    }
}