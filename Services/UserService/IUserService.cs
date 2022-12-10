using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using disclodo.Dtos.User;

namespace disclodo.Services.UserService;

public interface IUserService
{
    public Task<GetUserDto> AddUser(PostUserDto user);
    public Task<GetUserDto?> GetUserById(Guid id);
    public Task<List<GetUserDto>> GetUsers();
    public Task<bool> DeleteUser(Guid id);
}
