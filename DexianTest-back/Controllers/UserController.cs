using DexianTest_back.Models; 
using Microsoft.AspNetCore.Mvc;
using DexianTest_back.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DexianTest_back.Controllers
{
    [ApiController]
    [Authorize]
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
        public async Task CreateUser([FromBody] NewUserModel user)
        {
             await _userService.CreateAsync(user); 
        }

        [HttpPut("{codUser}")]
        public async Task UpdateUser(int codUser, [FromBody] NewUserModel user)
        {
            await _userService.UpdateAsync(codUser, user);
        }

        [HttpDelete("{codUser}")]
        public async Task DeleteUser(string id)
        {
            await _userService.DeleteAsync(id);
        }
    }
}
