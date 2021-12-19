using System.Collections.Generic;
using ScrumMasters.Webshop.Security.Model;

namespace ScrumMasters.Webshop.Security
{
    public interface IAuthRepository
    {
        public LoginUser IsValidUserInformation(LoginUser user);
        public LoginUser VerifyLoginUser(string email);
        public bool UserExists(UserDetails userDetails);
        public void SaveUser(LoginUser userEntity);
        List<Permission> GetPermissions(int userId);

    }
}