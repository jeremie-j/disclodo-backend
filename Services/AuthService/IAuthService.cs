using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using disclodo.Models;

namespace disclodo.Services.AuthService;

public interface IAuthService
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    public bool VerifyPasswordHash(User user, string password, byte[] passwordHash, byte[] passwordSalt);
    public string CreateToken(User user);
}
