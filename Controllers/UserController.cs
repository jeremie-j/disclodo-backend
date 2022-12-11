using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using disclodo.Dtos.User;
using disclodo.Services.UserService;


namespace disclodo.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetUserDto>>> GetAllUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUserById(Guid id)
        {
            var result = await _userService.GetUserById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GetUserDto>> AddUser([FromBody] PostUserDto user)
        {
            return Ok(await _userService.AddUser(user));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var success = await _userService.DeleteUser(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}