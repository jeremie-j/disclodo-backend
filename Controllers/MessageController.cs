using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using disclodo.Data;
using disclodo.Dtos.Message;
using disclodo.Services.MessageService;
namespace disclodo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("{channelId}")]
        public async Task<ActionResult<List<GetMessageDto>>> GetChannelMessages(Guid channelId)
        {
            return Ok(await _messageService.GetChannelMessages(channelId));
        }

        [HttpPost]
        public async Task<ActionResult<GetMessageDto>> AddMessage([FromBody] PostMessageDto message)
        {
            return Ok(await _messageService.AddMessage(message));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetMessageDto>> UpdateMessage([FromBody] PutMessageDto message, int id)
        {
            return Ok(await _messageService.UpdateMessage(message, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var success = await _messageService.DeleteMessage(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}