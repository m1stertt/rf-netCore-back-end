using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumMasters.Webshop.Core.Models
{
    public class User
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string StreetAndNumber { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
    }
}