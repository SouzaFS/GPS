using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GPS.DTOs{

    public class LocationDTO{
        
        [BsonElement("Latitude")]
        public double? Latitude { get; set; }

        [BsonElement("Longitude")]
        public double? Longitude { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [GraphQLType(typeof(IdType))]
        public required string UserId { get; set; }
        
    }
}