using DexianTest_back.Models;
using DexianTest_back.Services;
using Microsoft.AspNetCore.Mvc;
using DexianTest_back.Interfaces;

namespace DexianTest_back.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet()]
        public  async Task<List<UserModel>> GetAllUsers()
        {
            var users = await _userService.GetAsync();
            return users;
        }

        [HttpPost]
        public async Task CreateUser([FromBody] INewUser user)
        {
             await _userService.CreateAsync(user); 
        }
    }
}
