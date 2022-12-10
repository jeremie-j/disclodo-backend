using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace disclodo.Dtos.Message
{
    public class PostMessageDto
    {
        public string Content { get; set; } = String.Empty;
        public Guid AuthorId { get; set; }
        public Guid ChannelId { get; set; }
    }
}