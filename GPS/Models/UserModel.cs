using GPS.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GPS.Models{
    public class UserModel : BaseModel{

        [BsonElement("FirstName")]
        public string? FirstName { get; set; }
        [BsonElement("LastName")]
        public string? LastName { get; set; }
        [BsonElement("Username")]
        public string? Username { get; set; }
        [BsonElement("Email")]
        public string? Email { get; set; }
        [BsonElement("FederalID")]
        public string? FederalID { get; set; }
        [BsonElement("Location")]
        public virtual LocationModel? Location { get; set; }

    }
}
