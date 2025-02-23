using MongoDB.Bson.Serialization.Attributes;

namespace GPS.DTOs{

    public class LocationDTO{
        
        [BsonElement("Latitude")]
        public double Latitude { get; set; }

        [BsonElement("Longitude")]
        public double Longitude { get; set; }
    }
}