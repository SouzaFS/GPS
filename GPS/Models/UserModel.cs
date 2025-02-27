using GPS.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GPS.Models{
    public class UserModel : BaseModel{

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
    }
}
