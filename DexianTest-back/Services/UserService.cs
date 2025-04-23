using MongoDB.Driver; 
using MongoDB.Driver.Linq;
using Microsoft.Extensions.Options;
using DexianTest_back.Models;
using DexianTest_back.Interfaces;
using MongoDB.Bson;
using System;

namespace DexianTest_back.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<UserModel> _userCollection;
        private readonly IPasswordService _passwordService;

        public UserService(IOptions<DataBaseModel> databaseSettings, IPasswordService passwordService)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<UserModel>("User");
            _passwordService = passwordService;
        }

        public async Task<List<UserModel>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<UserModel?> GetByIdAsync(string id) =>
            await _userCollection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(NewUserModel user)
        {
            var existingUser = await _userCollection.Find(x => x.ICodUsuario == user.CodUser).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new InvalidOperationException($"Usuario com id {user.CodUser} já existe!");
            }
 
            string hashedPassword = _passwordService.HashPassword(user.Pass);

            var userToAdd = new UserModel()
            {
                ICodUsuario = user.CodUser,
                Id = ObjectId.GenerateNewId(),
                SNome = user.Name,
                SSenha = hashedPassword
            };

            await _userCollection.InsertOneAsync(userToAdd);
        }

        public async Task<bool> UpdateAsync(int codUser, NewUserModel updatedUser)
        {
            var existingUser = await _userCollection.Find(x => x.ICodUsuario == codUser).FirstOrDefaultAsync();
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"Usuario com código {codUser} não encontrado!");
            }

            if (updatedUser.Pass != null && updatedUser.Pass.Length >= 1)      
            { 
                existingUser.SSenha = _passwordService.HashPassword(updatedUser.Pass);
            }

            if (updatedUser.Name != null && updatedUser.Name.Length >= 1)       
            {
                existingUser.SNome = updatedUser.Name;
            }

            if (updatedUser.CodUser != 0)
            {
                existingUser.ICodUsuario = updatedUser.CodUser;
            }
            
            var result = await _userCollection.ReplaceOneAsync(x => x.ICodUsuario == codUser, existingUser);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existingUser = await _userCollection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"Usuario com id {id} não encontrado!");
            }
            
            var result = await _userCollection.DeleteOneAsync(x => x.Id == ObjectId.Parse(id));
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
