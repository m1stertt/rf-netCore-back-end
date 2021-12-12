using System.Collections.Generic;

namespace ScrumMasters.Webshop.WebAPI.Dtos.Auth
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public List<string> Permissions { get; set; }
        public string Email { get; set; }
    }
}