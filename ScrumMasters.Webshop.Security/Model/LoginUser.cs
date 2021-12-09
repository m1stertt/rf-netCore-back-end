using System.Collections.Generic;

namespace ScrumMasters.Webshop.Security.Model
{
    public class LoginUser
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] PasswordSalt { get; set; }
        
        public List<Permission> Permissions { get; set; }
        public int DbUserId { get; set; }
    }
}