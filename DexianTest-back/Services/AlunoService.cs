using DexianTest_back.Interfaces;
using DexianTest_back.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DexianTest_back.Services
{
    public class AlunoService : IAlunoService
    {  
        private static readonly List<AlunoModel> _alunosStatic = new List<AlunoModel>();
         
        private readonly List<AlunoModel> _alunos;
        
        public AlunoService()
        { 
            _alunos = _alunosStatic;
        }

        public async Task CreateAsync(NewAlunoModel aluno)
        {
            var alunoModel = new AlunoModel
            {
                Id = Guid.NewGuid().ToString(),
                CodAluno = aluno.CodAluno,
                Nome = aluno.Nome,
                DataNascimento = aluno.DataNascimento,
                CPF = aluno.CPF,
                Endereco = aluno.Endereco,
                Celular = aluno.Celular,
                CodEscola = aluno.CodEscola
            };
             
            if (_alunos.Find(x => x.CodAluno == aluno.CodAluno) != null)
            {
                throw new InvalidOperationException($"Aluno com código {alunoModel.CodAluno} já existe!");
            }

            _alunos.Add(alunoModel);

            await Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(int codAluno)
        {
            var aluno = _alunos.Find(x => x.CodAluno == codAluno);
            if (aluno == null)
            {
                throw new KeyNotFoundException($"Aluno com código {codAluno} não encontrado!");
            }

            _alunos.Remove(aluno);
            
            return true;
        }

        public async Task<List<AlunoModel>> GetAsync()
        {
            return _alunos;
        }

        public async Task<bool> UpdateAsync(string id, NewAlunoModel aluno)
        {
            var existingAluno = _alunos.Find(x => x.Id == id);
            if (existingAluno == null)
            {
                throw new KeyNotFoundException($"Aluno com código {id} não encontrado!");
            }

            if (!string.IsNullOrWhiteSpace(aluno.Nome))
            {
                existingAluno.Nome = aluno.Nome;
            }

            if (aluno.CodAluno != 0 && aluno.CodAluno != null)
            {  
                if (_alunos.Any(x => x.CodAluno == aluno.CodAluno))
                {
                    throw new InvalidOperationException($"Aluno com código {aluno.CodAluno} já existe!");
                }
                
                existingAluno.CodAluno = aluno.CodAluno;
            }

            if (aluno.DataNascimento != default)
            {
                existingAluno.DataNascimento = aluno.DataNascimento;
            }

            if (!string.IsNullOrWhiteSpace(aluno.CPF))
            {
                existingAluno.CPF = aluno.CPF;
            }

            if (!string.IsNullOrWhiteSpace(aluno.Endereco))
            {
                existingAluno.Endereco = aluno.Endereco;
            }

            if (!string.IsNullOrWhiteSpace(aluno.Celular))
            {
                existingAluno.Celular = aluno.Celular;
            }

            if (aluno.CodEscola != 0)
            {
                existingAluno.CodEscola = aluno.CodEscola;
            }
            
            return true;
        }

        public async Task<List<AlunoModel>> GetByName(string nameOrCpf)
        {
            var alunos = new List<AlunoModel>();

            bool isNumber = int.TryParse(nameOrCpf, out int numericValue);
            if (isNumber) 
            {
                alunos = _alunos
                    .Where(x => x.CPF.Contains(nameOrCpf))
                    .ToList();
            } 
            else
            {
                alunos = _alunos
                    .Where(x => x.Nome.ToLower().Contains(nameOrCpf.ToLower()))
                    .ToList();
            }
            
            if (alunos.Count == 0)
            {
                throw new KeyNotFoundException($"Nenhum aluno encontrado");
            }
            
            return alunos;
        }
    }
}
