using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GPS.DTOs{
    public class UserDTO{

        [BsonElement("FirstName")]
        public string? FirstName {get; set; }

        [BsonElement("LastName")]
        public string? LastName {get; set; }

        [BsonElement("Username")]
        public string? Username {get; set; }

        [BsonElement("Email")]
        public string? Email {get; set; }

        [BsonElement("FederalID")]
        public string? FederalID {get; set; }

        [BsonElement("Nationality")]
        public string? Nationality {get; set; }

        [BsonElement("Location")]
        public virtual LocationDTO? Location { get; set; }


    }
}
