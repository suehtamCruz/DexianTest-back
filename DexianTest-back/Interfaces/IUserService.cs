using DexianTest_back.Models;

namespace DexianTest_back.Interfaces
{
    public interface IUserService
    { 
        Task<List<UserModel>> GetAsync();  
        Task CreateAsync(NewUserModel user); 
        Task<bool> UpdateAsync(int codUser, NewUserModel user); 
        Task<bool> DeleteAsync(int codUser);
    }
}
