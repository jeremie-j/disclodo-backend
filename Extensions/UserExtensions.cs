using disclodo.Dtos.User;
using disclodo.Models;

namespace disclodo.UserExtensions;

public static class UserExtensions
{
    public static User ToUser(this PostUserDto user)
    {
        return new User
        {
            Username = user.Username,
            ProfilePicture = user.ProfilePicture
        };
    }

    public static GetUserDto ToGetUserDto(this User user)
    {
        return new GetUserDto
        {
            Id = user.Id,
            Username = user.Username,
            ProfilePicture = user.ProfilePicture,
            CreatedAt = user.CreatedAt
        };
    }

    public static User ApplyPutUserDto(this User user, PutUserDto putUserDto)
    {
        user.Username = putUserDto.Username;
        user.ProfilePicture = putUserDto.ProfilePicture;
        return user;
    }

}