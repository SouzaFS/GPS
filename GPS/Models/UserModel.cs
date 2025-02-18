using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GPS.Models{
    public class UserModel{

        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [GraphQLType(typeof(IdType))]
        [BsonIgnoreIfDefault]
        public string Id {get; set; }

        [BsonElement("FirstName")]
        public required string FirstName {get; set; }

        [BsonElement("LastName")]
        public required string LastName {get; set; }

        [BsonElement("Username")]
        public required string Username {get; set; }

        [BsonElement("Email")]
        public required string Email {get; set; }

        [BsonElement("FederalID")]
        public required string FederalID {get; set; }
    }
}
