using System.Collections.Generic;

namespace ScrumMasters.Webshop.Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public List<Product> Products { get; set; }
    }
}