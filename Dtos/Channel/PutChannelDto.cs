using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace disclodo.Dtos.Channel
{
    public class PutChannelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}