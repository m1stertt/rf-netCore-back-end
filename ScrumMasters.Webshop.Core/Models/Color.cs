using System.Collections.Generic;

namespace ScrumMasters.Webshop.Core.Models
{
    public class Color
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public List<Product> Products { get; set; }
    }
}