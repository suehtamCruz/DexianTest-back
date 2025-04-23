using DexianTest_back.Models;

namespace DexianTest_back.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserModel user);
    }
}
