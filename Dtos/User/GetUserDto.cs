using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace disclodo.Dtos.User
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string? ProfilePicture { get; set; } = null;
        public DateTime CreatedAt { get; set; }
    }
}