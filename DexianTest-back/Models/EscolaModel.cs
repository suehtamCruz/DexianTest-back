using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DexianTest_back.Models
{
    public class EscolaModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId? Id { get; set; }

        [BsonElement("iCodEscola")]
        public int ICodEscola { get; set; }

        [BsonElement("sDescricao")]
        public string SDescricao { get; set; } 
    }
}
