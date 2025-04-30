using DexianTest_back.Models;
using DexianTest_back.Interfaces; 
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DexianTest_back.Services
{
    public class UserService : IUserService
    { 
        private static readonly List<UserModel> _usersStatic = new List<UserModel>();
          
        private readonly List<UserModel> _users;
        private readonly IPasswordService _passwordService;

        public UserService(IPasswordService passwordService)
        {
            _users = _usersStatic;
            _passwordService = passwordService;
             
            if (_users.Count == 0)
            {
                var testUser = new UserModel() 
                {  
                    ICodUsuario = 0, 
                    SNome = "Teste", 
                    SSenha = _passwordService.HashPassword("123"), 
                    Id = Guid.NewGuid().ToString() 
                };
                _users.Add(testUser);
            }
        }

        public Task<List<UserModel>> GetAsync() 
        {
            return Task.FromResult(_users);
        }

        public Task CreateAsync(NewUserModel user)
        {
            if (_users.Any(x => x.ICodUsuario == user.CodUser))
            {
                throw new InvalidOperationException($"Usuario com id {user.CodUser} já existe!");
            }
 
            string hashedPassword = _passwordService.HashPassword(user.Pass);

            var userToAdd = new UserModel()
            {
                ICodUsuario = user.CodUser,
                Id = Guid.NewGuid().ToString(),
                SNome = user.Name,
                SSenha = hashedPassword
            };

            _users.Add(userToAdd);
            
            return Task.CompletedTask;
        }

        public Task<bool> UpdateAsync(int codUser, NewUserModel updatedUser)
        {
            var existingUser = _users.Find(x => x.ICodUsuario == codUser);
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

            if (updatedUser.CodUser != 0 && updatedUser.CodUser != codUser)
            { 
                if (_users.Any(x => x.ICodUsuario == updatedUser.CodUser))
                {
                    throw new InvalidOperationException($"Usuario com código {updatedUser.CodUser} já existe!");
                }
                
                existingUser.ICodUsuario = updatedUser.CodUser;
            }
            
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int codUser)
        {
            var user = _users.Find(x => x.ICodUsuario == codUser);
            if (user == null)
            {
                throw new KeyNotFoundException($"Usuario com id {codUser} não encontrado!");
            }
            
            _users.Remove(user);
            
            return Task.FromResult(true);
        }
    }
}
