using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GPS.Models.Base{

    public class BaseModel{
        
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [GraphQLType(typeof(IdType))]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}