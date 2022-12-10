using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace disclodo.Dtos.Channel
{
    public class PostChannelDto
    {
        public string Name { get; set; } = null!;
        public List<Guid> ParticipantIds { get; set; } = null!;
    }
}