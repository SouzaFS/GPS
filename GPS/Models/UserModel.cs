using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace GPS.Models{
    public class UserModel{

        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [GraphQLType(typeof(IdType))]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FirstName")]
        public required string FirstName { get; set; }

        [BsonElement("LastName")]
        public required string LastName { get; set; }

        [BsonElement("Username")]
        public required string Username { get; set; }

        [BsonElement("Email")]
        public required string Email { get; set; }

        [BsonElement("FederalID")]
        public required string FederalID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string LocationId { get; set; } = null;

        [GraphQLIgnore]
        public IMongoCollection<LocationModel> Location { get; set; }

        [UseFirstOrDefault]
        public async Task<LocationModel> GetLocation([Service] IMongoCollection<LocationModel> locationCollection){
            return await locationCollection.Find(a => a.UserId == Id).FirstOrDefaultAsync();
        }
    }
}
