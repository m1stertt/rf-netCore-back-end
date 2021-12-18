using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScrumMasters.Webshop.Security.Model;

namespace ScrumMasters.Webshop.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, IAuthRepository authRepository)
        {
            _authRepository = authRepository ?? throw new InvalidDataException("ProductRepository Cannot Be Null");
            _configuration = configuration;
        }

        public bool UserExists(UserDetails userDetails)
        {
            return _authRepository.UserExists(userDetails);
        }

        private LoginUser IsValidUserInformation(LoginUser user)
        {
            return _authRepository.IsValidUserInformation(user);
        }

        /// <summary>
        /// Generate JWT Token after successful login.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public string GenerateJwtToken(LoginUser user)
        {
            var userFound = IsValidUserInformation(user);
            if (userFound == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", userFound.Id.ToString()),
                    new Claim("Email", userFound.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = _configuration["JwtConfig:Issuer"],
                Audience = _configuration["JwtConfig:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public byte[] VerifyLogin(string email, string password)
        {
            LoginUser user = _authRepository.VerifyLoginUser(email);
            if (user == null || !VerifyHash(password, user.HashedPassword, user.PasswordSalt)) return null;
            return user.HashedPassword;
        }

        private bool VerifyHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public static void CreateHashAndSalt(string password, out byte[] passwordHash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public List<Permission> GetPermissions(int userId)
        {
            return _authRepository.GetPermissions(userId);
        }

        public LoginUser CreateLoginUser(UserDetails userDetails)
        {
            CreateHashAndSalt(userDetails.Password, out var passwordHash, out var salt);
            var createdEntity = new LoginUser()
            {
                Email = userDetails.Email,
                HashedPassword = passwordHash,
                PasswordSalt = salt,
            };
            _authRepository.SaveUser(createdEntity);
            return createdEntity;

        }
    }
}