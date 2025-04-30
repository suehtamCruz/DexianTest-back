using System;

namespace DexianTest_back.Models
{
    public class NewAlunoModel
    {
        public int? CodAluno { get; set; }

        public string? Nome { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string? CPF { get; set; }

        public string? Endereco { get; set; }

        public string? Celular { get; set; }

        public int? CodEscola { get; set; }
    }
}
