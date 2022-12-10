using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace disclodo.Dtos.Message
{
    public class PutMessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = String.Empty;
    }
}