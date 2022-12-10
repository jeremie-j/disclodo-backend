using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using disclodo.Models;
using disclodo.Data;

namespace disclodo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly DataContext _context;
        private static Message message = new Message();
        private static List<Message> messageList = new List<Message>{
            new Message{Id= 1,Content = "HELLO"},
            new Message{Content = "Coucou"}
        };

        public MessageController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _context.User.ToList();
            // var user = _context.User.Add(new User { Username = "test" });
            // _context.SaveChanges();
            return Ok(users);
        }

        [HttpGet("/test")]
        public ActionResult<User> GetOne()
        {
            var user = new User { Username = "test" };
            _context.User.Add(user);
            _context.SaveChanges();
            return Ok(user);
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