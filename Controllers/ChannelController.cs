using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using disclodo.Dtos.User;
using disclodo.Dtos.Channel;
using disclodo.Services.ChannelService;


namespace disclodo.Controllers
{
    [Route("api/[controller]")]
    public class ChannelController : Controller
    {

        private readonly IChannelService _channelService;

        public ChannelController(IChannelService userService)
        {
            _channelService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<GetChannelDto>>> GetUserChannels(Guid userId)
        {
            return Ok(await _channelService.GetUserChannels(userId));
        }


        [HttpPost]
        public async Task<ActionResult<GetChannelDto>> AddChannel([FromBody] PostChannelDto channel)
        {
            return Ok(await _channelService.AddChannel(channel));
        }

        [HttpPost("addUsers")]
        public async Task<ActionResult<GetChannelDto>> AddUsersToChannel(Guid channelId, [FromBody] List<Guid> userIds)
        {
            return Ok(await _channelService.AddUsersToChannel(channelId, userIds));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChannel(Guid id)
        {
            var success = await _channelService.DeleteChannel(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}