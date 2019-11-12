using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //POST api/Users/register
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]Users users)
        {
            try
            {
                var UserExits = _userService.IsUserExits(users.UserName);
                if (UserExits)
                {
                    return BadRequest(new {Message = "User Name is exits!"});
                }

                var result = await _userService.AddUserAsync(users);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        //GET api/Users/id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Users>> GetUserById(string id)
        {
            var currentUser = await _userService.GetListUser(id);
            return new ActionResult<Users>(currentUser);
        }
    }
}