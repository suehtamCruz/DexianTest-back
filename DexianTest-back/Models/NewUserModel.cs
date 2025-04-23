using DexianTest_back.Interfaces;

namespace DexianTest_back.Models
{
    public class NewUserModel : INewUser
    {
        public string Name { get; set; }
        public int CodUser { get; set; }
        public string Pass { get; set; }
    }
}
