using GPS.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GPS.Models{

    public class LocationModel : BaseModel{

        [BsonElement("Latitude")]
        public double? Latitude { get; set; }

        [BsonElement("Longitude")]
        public double? Longitude { get; set; } 
    }
}