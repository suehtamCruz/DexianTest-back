using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DexianTest_back.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? Id { get; set; }

        [BsonElement("sNome")]
        public string Name { get; set; }

        [BsonElement("iCodUsuario")]
        public int CodUser { get; set; }

        [BsonElement("sSenha")]
        public string? Pass { get; set; }
         
  
    }
}
