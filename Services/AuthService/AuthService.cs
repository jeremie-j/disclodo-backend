using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using disclodo.Models;
namespace disclodo.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration config;

        public AuthService(IConfiguration config)
        {
            this.config = config;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(User user, string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
                {
                new Claim("username", user.Username),
                new Claim("id", user.Id.ToString())
                };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public string CreateSubscriberMercureToken(List<Channel> channels)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config.GetSection("AppSettings:MercureToken").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(new SecurityTokenDescriptor{
                Claims = new Dictionary<string, object>{
                    {"mercure", 
                    new Dictionary<string, object> { 
                        {"publish", new List<string>() },
                        {"subscribe", channels.Select(c => c.Id.ToString()).ToList()
                        }}}
                },
                SigningCredentials = creds
            });

            System.Console.WriteLine(handler.WriteToken(token));
            return handler.WriteToken(token);
        }

        public string CreatePublisherMercureToken()
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config.GetSection("AppSettings:MercureToken").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(new SecurityTokenDescriptor{
                Claims = new Dictionary<string, object>{
                    {"mercure", 
                    new Dictionary<string, object> { 
                        {"publish", new List<string> { "*" } },
                        {"subscribe", new List<string>() 
                        }}}
                },
                SigningCredentials = creds
            });

            System.Console.WriteLine(handler.WriteToken(token));
            return handler.WriteToken(token);
        }

        public class MercureClaims
        {
            public List<string>? publish { get; set; }
            public List<string>? subscribe { get; set; }
        }
    }
}