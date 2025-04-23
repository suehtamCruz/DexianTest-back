using MongoDB.Bson.Serialization.Attributes;

namespace DexianTest_back.Models
{
    public class NewAlunoModel
    {
        [BsonElement("iCodAluno")]
        public int CodAluno { get; set; }

        [BsonElement("sNome")]
        public string Nome { get; set; }

        [BsonElement("dNascimento")]
        public DateTime DataNascimento { get; set; }

        [BsonElement("sCPF")]
        public string CPF { get; set; }

        [BsonElement("sEndereco")]
        public string Endereco { get; set; }

        [BsonElement("sCelular")]
        public string Celular { get; set; }

        [BsonElement("iCodEscola")]
        public int CodEscola { get; set; }
    }
}
