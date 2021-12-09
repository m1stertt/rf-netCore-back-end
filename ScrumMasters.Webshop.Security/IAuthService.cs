using System.Collections.Generic;
using ScrumMasters.Webshop.Security.Model;

namespace ScrumMasters.Webshop.Security
{
    public interface IAuthService
    {
        string GenerateJwtToken(LoginUser userUserName);
        public byte[] VerifyLogin(string username, string password);
        public static void CreateHashAndSalt(string password, out byte[] passwordHash, out byte[] salt)
        {
            throw new System.NotImplementedException(); //@todo Maybe fix this, just quick fix for now to easily call this method during initialization of database.
        }

        List<Permission> GetPermissions(int userId);
        void RegisterUser(UserDetails userDetails);
    }
}