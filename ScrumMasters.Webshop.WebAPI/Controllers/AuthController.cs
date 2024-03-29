﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumMasters.Webshop.Security;
using ScrumMasters.Webshop.Security.Model;
using ScrumMasters.Webshop.WebAPI.Dtos.Auth;

namespace ScrumMasters.Webshop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var dtos = dto;
            var tokenString = _authService.GenerateJwtToken(new LoginUser
            {
                Email = dto.Email,
                HashedPassword = _authService.VerifyLogin(dto.Email, dto.Password)
            });
            if (string.IsNullOrEmpty(tokenString))
            {
                return StatusCode(401, "Please use a valid Username and Password");
            }

            return Ok(new {Token = tokenString, Message = "Success"});
        }

        [HttpGet(nameof(GetProfile))]
        public ActionResult<ProfileDto> GetProfile()
        {
            var user = HttpContext.Items["LoginUser"] as LoginUser;
            if (user != null)
            {
                List<Permission> permissions = _authService.GetPermissions(user.Id);
                return Ok(new ProfileDto
                {
                    Id = user.Id,
                    Permissions = permissions.Select(p => p.Name).ToList(),
                    Email = user.Email
                });
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost(nameof(RegisterUser))]
        public IActionResult RegisterUser([FromBody] UserDetails userDetails)
        {
            
            if (userDetails == null)
            {
                return BadRequest("User details is required to register a new user.");
            }

            if (!_authService.UserExists(userDetails))
            {
                _authService.CreateLoginUser(userDetails);
                return Ok();
            }

            return StatusCode(403, "User already exists.");

        }
    }
}