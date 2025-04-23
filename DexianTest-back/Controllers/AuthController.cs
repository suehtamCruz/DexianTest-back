using DexianTest_back.Interfaces;
using DexianTest_back.Models;
using Microsoft.AspNetCore.Mvc; 
using MongoDB.Bson;

namespace DexianTest_back.Controllers
{
    [ApiController] 
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IPasswordService _passwordService;

        public AuthController(IUserService userService, IJwtService jwtService, IPasswordService passwordService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _passwordService = passwordService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        { 
            var users = await _userService.GetAsync();
            var user = users.FirstOrDefault(u => u.SNome == loginModel.Nome);

            if (user == null)
            {
                return Unauthorized(new { message = "Credenciais inválidas" });
            }
 
            bool isPasswordValid = _passwordService.VerifyPassword(loginModel.Password, user.SSenha);
            
            if (!isPasswordValid)
            {
                return Unauthorized(new { message = "Credenciais inválidas" });
            }
 
            var token = _jwtService.GenerateToken(user);

            return Ok(new 
            { 
                token,
                user = new
                {
                    id = user.Id.ToString(),
                    name = user.SNome,
                    codUser = user.ICodUsuario
                }
            });
        }
    }
}
