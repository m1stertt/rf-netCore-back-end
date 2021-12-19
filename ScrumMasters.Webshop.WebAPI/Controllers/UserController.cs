using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Security.Model;
using ScrumMasters.Webshop.WebAPI.PolicyHandlers;

namespace ScrumMasters.Webshop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new InvalidDataException("UserService Cannot Be Null.");
        }
        
        [Authorize(Policy = nameof(IsLoggedInHandler))]
        [HttpGet("{id:int}")]
        public ActionResult<User> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to find a user by it's ID in the repository.");
            }

            if (_userService.GetUserById(id) != null)
            {
                return Ok(_userService.GetUserById(id));
            }

            return StatusCode(404, "User not found.");
        }

        [Authorize(Policy = nameof(IsLoggedInHandler))]
        [HttpPost]  
        public ActionResult<User> Post([FromBody] User newUser)
        {

            if (newUser == null)
            {
                return BadRequest("A user is required before creating a user in the repository.");
            }

            if (!_userService.CheckUserByEmail(newUser.Email))
            {
                return Ok(_userService.Create(newUser));
            }

            return StatusCode(403, "User already exist.");
        }

        [Authorize(Policy = nameof(IsLoggedInHandler))]
        [HttpPut("{id}")]  
        public ActionResult<User> Update(int id, [FromBody] User newUser)
        {
            
            if (id < 1 || id != newUser.Id)
            {
                return BadRequest("Correct id is needed to update a user in the repository.");
            }

            return Ok(_userService.Update(newUser));
        }

        [Authorize(Policy = nameof(IsLoggedInHandler))]
        [HttpDelete]
        public ActionResult<User> Delete()
        {
            var user = HttpContext.Items["LoginUser"] as LoginUser;
            if (user == null)
            {
                return BadRequest("You need to be logged in to do this delete request.");
            }
            return Ok(_userService.DeleteById(user.Id));
        }

    }
}