using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace disclodo.Dtos.User
{
    public class PutUserDto
    {
        public string Username { get; set; } = null!;
        public string? ProfilePicture { get; set; } = null;
    }
}