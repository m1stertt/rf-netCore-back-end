namespace ScrumMasters.Webshop.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public object FirstName { get; set; }
        public object LastName { get; set; }
        public object Email { get; set; }
        public object PostalCode { get; set; }
        public object StreetAndNumber { get; set; }
        public object City { get; set; }
        public object PhoneNumber { get; set; }
    }
}