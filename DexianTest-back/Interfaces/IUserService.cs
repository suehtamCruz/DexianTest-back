using DexianTest_back.Models;

namespace DexianTest_back.Interfaces
{
    public interface IUserService
    { 
        Task<List<UserModel>> GetAsync(); 
        Task<UserModel?> GetByIdAsync(string id); 
        Task CreateAsync(INewUser user); 
        Task<bool> UpdateAsync(string id, INewUser user); 
        Task<bool> DeleteAsync(string id);
    }
}
