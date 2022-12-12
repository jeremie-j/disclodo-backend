namespace disclodo.Dtos.User
{
    public class LoginUserDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class RegisterUserDto : LoginUserDto { }
}