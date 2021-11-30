using System.Collections.Generic;

namespace ScrumMasters.Webshop.WebAPI.Dtos.Auth
{
    public class ProfileDto
    {
        public List<string> Permissions { get; set; }
        public string Name { get; set; }
    }
}