using DexianTest_back.Interfaces;
using DexianTest_back.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DexianTest_back.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IMongoCollection<AlunoModel> _alunoCollection;

        public AlunoService(IOptions<DataBaseModel> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _alunoCollection = mongoDatabase.GetCollection<AlunoModel>("Student");
        }

        public async Task CreateAsync(NewAlunoModel aluno)
        { 
            var alunoModel = new AlunoModel
            {
                Id = ObjectId.GenerateNewId(),
                CodAluno = aluno.CodAluno,
                Nome = aluno.Nome,
                DataNascimento = aluno.DataNascimento,
                CPF = aluno.CPF,
                Endereco = aluno.Endereco,
                Celular = aluno.Celular,
                CodEscola = aluno.CodEscola
            };

            await _alunoCollection.InsertOneAsync(alunoModel);
        }

        public async Task<bool> DeleteAsync(int codAluno)
        {
            var existingAluno = await _alunoCollection.Find(x => x.CodAluno == codAluno).FirstOrDefaultAsync();
            if (existingAluno == null)
            {
                throw new KeyNotFoundException($"Aluno com código {codAluno} não encontrado!");
            }
            var result = await _alunoCollection.DeleteOneAsync(x => x.CodAluno == codAluno);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<List<AlunoModel>> GetAsync()
        {
            return await _alunoCollection.Find(_ => true).ToListAsync();
        }
        public async Task<AlunoModel?> GetByIdAsync(string id)
        {
            return await _alunoCollection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(int codAluno, NewAlunoModel aluno)
        { 
             var existingAluno = await _alunoCollection.Find(x => x.CodAluno == codAluno).FirstOrDefaultAsync();
            if (existingAluno == null)
            {
                throw new KeyNotFoundException($"Aluno com código {codAluno} não encontrado!");
            }
 
            if (!string.IsNullOrWhiteSpace(aluno.Nome))
            {
                existingAluno.Nome = aluno.Nome;
            }

            if (aluno.CodAluno != 0)
            {
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
            var result = await _alunoCollection.ReplaceOneAsync(x => x.CodAluno == codAluno, existingAluno);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
