using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScrumMasters.Webshop.Security.Model;

namespace ScrumMasters.Webshop.Security
{
    public class AuthRepository : IAuthRepository
    {
        
        private readonly AuthDbContext _ctx;

        public AuthRepository(AuthDbContext context)
        {
            _ctx = context ?? throw new InvalidDataException("Auth Repository Must have a DBContext");
        }

        public bool UserExists(UserDetails userDetails)
        {
            if (_ctx.LoginUsers.Any(user => userDetails.Email.Equals(user.Email)))
            {
                return true;
            }

            return false;
        }

        
        public LoginUser IsValidUserInformation(LoginUser user)
        {
            return _ctx.LoginUsers.FirstOrDefault(u =>
                u.Email.Equals(user.Email) && u.HashedPassword.Equals(user.HashedPassword));
        }

        public LoginUser VerifyLoginUser(string email)
        {
            return _ctx.LoginUsers.FirstOrDefault(u => u.Email.Equals(email));
        }

        public List<Permission> GetPermissions(int userId)
        {
            return _ctx.UserPermissions
                .Include(up => up.Permission)
                .Where(up => up.UserId == userId)
                .Select(up => up.Permission)
                .ToList();
        }

        public void SaveUser(LoginUser userEntity)
        {
            var savedEntity = _ctx.LoginUsers.Add(userEntity).Entity;
            _ctx.SaveChanges();
        }
    }
}