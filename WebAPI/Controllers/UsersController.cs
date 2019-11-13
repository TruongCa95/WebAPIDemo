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

        //POST api/users/register
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
        // PUT api/users/edit
        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> Update(Users user)
        {
            try
            {
                var existUser = _userService.IsUserExits(user.UserName);
                if (existUser)
                {
                    return BadRequest(new { message = "Name is exist!" });
                }
                var result = await _userService.UpdateUserAsync(user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        // POST api/users/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(Login login)
        {
            var user = await _userService.UserAuthentication(login.Email, login.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        //GET api/Users/id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var currentUser = await _userService.GetListUser(id);
            return new ActionResult<Users>(currentUser);
        }

        //GET api/Users/all
        [HttpGet("all")]
        public ActionResult GetList()
        {
            return Ok(_userService.GetUsers());
        }
        public class Login
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}