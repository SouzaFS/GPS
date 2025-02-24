using GPS.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GPS.DTOs{
    public class UserDTO{

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

        [BsonRepresentation(BsonType.ObjectId)]
        [GraphQLType(typeof(IdType))]
        public required string LocationId { get; set; }

        public virtual LocationDTO? Location { get; set; }
    }
}
