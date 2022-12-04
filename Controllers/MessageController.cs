using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using disclodo.Models;

namespace disclodo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private static Message message = new Message();
        private static List<Message> messageList = new List<Message>{
            new Message{Id= 1,Content = "HELLO"},
            new Message{Content = "Coucou"}
    };

        [HttpGet("all")]
        public ActionResult<List<Message>> GetAll()
        {
            return Ok(messageList);
        }

        [HttpGet("{id}")]
        public ActionResult<Message> GetById(int id)
        {
            var result = messageList.FirstOrDefault(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}