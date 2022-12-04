using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace disclodo.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = String.Empty;
        public int SenderId { get; set; }
        public int ChannelId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}