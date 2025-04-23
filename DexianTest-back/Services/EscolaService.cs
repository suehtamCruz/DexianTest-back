using DexianTest_back.Interfaces;
using DexianTest_back.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DexianTest_back.Services
{
    public class EscolaService : IEscolaService
    {
        private readonly IMongoCollection<EscolaModel> _escolaCollection;

        public EscolaService (IOptions<DataBaseModel> databaseSettings)
        {
            var mongoClient = new MongoClient(
               databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _escolaCollection = mongoDatabase.GetCollection<EscolaModel>("School");
        }

        public async Task CreateAsync(NewEscolaModel escola)
        {
            var existingEscola = _escolaCollection.Find(x => x.ICodEscola == escola.Code).FirstOrDefault();
            if (existingEscola != null)
            {
                throw new InvalidOperationException($"Escola com id {escola.Code} já existe!");
            }
            var newSchool = new EscolaModel
            {
                ICodEscola = escola.Code,
                SDescricao = escola.Description,
                Id = ObjectId.GenerateNewId()
            };

            await _escolaCollection.InsertOneAsync(newSchool);
        }

        public async Task<bool> DeleteAsync(int codEscola)
        {
            var existingEscola = await _escolaCollection.FindAsync(x => x.ICodEscola == codEscola);
            if (existingEscola == null)
            {
                throw new KeyNotFoundException($"Escola com código {codEscola} não encontrada!");
            }

            var result = await _escolaCollection.DeleteOneAsync(x => x.ICodEscola == codEscola);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<List<EscolaModel>> GetAsync()
        {
            return await _escolaCollection.Find(_ => true).ToListAsync();
        }

        public Task<EscolaModel> GetByIdAsync(string id)
        {
            var escola = _escolaCollection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefault();
            if (escola == null)
            {
                throw new KeyNotFoundException($"Escola com id {id} não encontrada!");
            }

            return Task.FromResult(escola);
        }

        public Task<bool> UpdateAsync(int codEscola, NewEscolaModel escola)
        {
            var existingEscola = _escolaCollection.Find(x => x.ICodEscola == codEscola).FirstOrDefault();

            if (existingEscola == null)
            {
                throw new KeyNotFoundException($"Escola com código {codEscola} não encontrada!");
            }

            if (escola.Code != 0)
            {
                existingEscola.ICodEscola = escola.Code;
            }
            if (!string.IsNullOrWhiteSpace(escola.Description))
            {
                existingEscola.SDescricao = escola.Description;
            }
            var result = _escolaCollection.ReplaceOne(x => x.ICodEscola == codEscola, existingEscola);

            return Task.FromResult(result.IsAcknowledged && result.ModifiedCount > 0);
        }
    }
}
