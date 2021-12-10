using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
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
        

        [HttpGet("{id:int}")]
        public ActionResult<User> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to find a User by it's ID in the repository.");
            }

            return Ok(_userService.GetUserById(id));
        }

        [Authorize(Policy = nameof(CanManageAccountHandler))]
        [HttpPost]  
        public ActionResult<User> Post([FromBody] User User)
        {
            if (User == null)
            {
                return BadRequest("A User is required before creating a User in the repository.");
            }
                
            return Ok(_userService.Create(User));
        }

        [Authorize(Policy = nameof(CanManageAccountHandler))]
        [HttpPut("{id}")]  
        public ActionResult<User> Update(int id, [FromBody] User User)
        {
            if (id < 1 || id != User.Id)
            {
                return BadRequest("Correct id is needed to update a User in the repository.");
            }

            return Ok(_userService.Update(User));
        }

        [Authorize(Policy = nameof(CanManageUsersHandler))]
        [HttpDelete("{id:int}")]
        public ActionResult<User> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to delete by id.");
            }
            return Ok(_userService.DeleteById(id));
        }

    }
}