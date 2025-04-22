using MongoDB.Driver; 
using MongoDB.Driver.Linq;
using Microsoft.Extensions.Options;
using DexianTest_back.Models;
using DexianTest_back.Interfaces;
using MongoDB.Bson;

namespace DexianTest_back.Services
{
    public class UserService : IUserService
    { 
        private readonly IMongoCollection<UserModel> _userCollection;  

        public UserService(IOptions<DataBaseModel> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<UserModel>("User");
        }

        public async Task<List<UserModel>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<UserModel?> GetByIdAsync(string id) =>
            await _userCollection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(INewUser user)
        {
            var userToAdd = new UserModel()
            {
                CodUser = user.CodUser,
                Id = ObjectId.GenerateNewId(),
                Name = user.Name,
                Pass = user.Pass
            };

            await _userCollection.InsertOneAsync(userToAdd);
        
        }

        public async Task<bool> UpdateAsync(string id, INewUser updatedUser)
        {
            var userToUpdate = new UserModel()
            {
                CodUser = updatedUser.CodUser,
                Id = ObjectId.GenerateNewId(),
                Name = updatedUser.Name,
                Pass = updatedUser.Pass
            };

            var result = await _userCollection.ReplaceOneAsync(x => x.Id == ObjectId.Parse(id), userToUpdate);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _userCollection.DeleteOneAsync(x => x.Id == ObjectId.Parse(id));
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
