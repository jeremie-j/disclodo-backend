
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;

using disclodo.Services.UserService;
using disclodo.Services.AuthService;
using disclodo.Models;
using disclodo.Dtos.User;

namespace webapi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;

        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterUserDto request)
        {
            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            var createdUser = await _userService.RegisterUser(user);

            string token = _authService.CreateToken(user);
            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserDto request)
        {
            var user = await _userService.GetUserByUsername(request.Username);

            if (user == null || user.Username != request.Username)
            {
                return BadRequest("User not found");
            }
            if (!_authService.VerifyPasswordHash(user, request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Incorrect password");
            }

            string token = _authService.CreateToken(user);
            return Ok(token);
        }
    }
}
