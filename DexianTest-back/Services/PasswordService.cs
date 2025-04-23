using DexianTest_back.Interfaces;
using BCrypt.Net;

namespace DexianTest_back.Services
{
    public class PasswordService : IPasswordService
    { 
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }
 
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
