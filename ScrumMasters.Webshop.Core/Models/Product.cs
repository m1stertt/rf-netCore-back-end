using System.Collections.Generic;

namespace ScrumMasters.Webshop.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public string ProductName { get; set; }
        
        public double ProductPrice { get; set; }
        
        public string ProductDescription { get; set; }
        
        public string ProductImageUrl { get; set; }

        public List<Category> Categories { get; set; }
    }
}