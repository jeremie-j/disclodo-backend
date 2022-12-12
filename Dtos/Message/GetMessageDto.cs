using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace disclodo.Dtos.Message
{
    public class GetMessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; } = null!;
        public Guid ChannelId { get; set; }
    }
}