using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using disclodo.Dtos.User;
using disclodo.Models;
namespace disclodo.Services.UserService;

public interface IUserService
{
    public Task<GetUserDto> RegisterUser(User user);
    public Task<GetUserDto?> GetUserById(Guid id);
    public Task<List<GetUserDto>> GetUsers();
    public Task<bool> DeleteUser(Guid id);
    public Task<User?> GetUserByUsername(string username);
    public Task<List<GetUserDto>> SearchUser(string username);
}
