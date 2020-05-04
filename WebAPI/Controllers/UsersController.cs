using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        [EnableCors]
        [AllowAnonymous]
        [Route("register")]
        public async Task<Users> Register(Users users)
        {
            try
            {
                var UserExits = _userService.IsUserExits(users.UserName);
                if (UserExits) return null;
                var result = await _userService.AddUserAsync(users);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //POST api/users/delete
        [HttpDelete]
        [EnableCors]
        [AllowAnonymous]
        [Route("delete")]
        public async Task<Users> Delete(Users users)
        {
            try
            {
                var UserExits = _userService.IsUserExits(users.UserName);
                if (!UserExits) return null;
                var result = await _userService.DeleteUserAsync(users);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        // PUT api/users/edit
        [Authorize]
        [EnableCors]
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
        [EnableCors]
        [HttpPost("login")]
        public async Task<Users> Authenticate(Login login)
        {
            var user = await _userService.UserAuthentication(login.Email, login.Password);
            return user;
        }
        //GET api/Users/id
        [HttpGet("{id}")]
        [Authorize]
        [EnableCors]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var currentUser = await _userService.GetListUser(id);
            return new ActionResult<Users>(currentUser);
        }

        //GET api/Users/all
        [HttpGet("all")]
        [EnableCors]
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