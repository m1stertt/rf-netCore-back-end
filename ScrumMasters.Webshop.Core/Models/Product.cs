using System.Collections.Generic;

namespace ScrumMasters.Webshop.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double ProductDiscountPrice { get; set; }
        public string ProductDescription { get; set; }
        public bool ProductFeatured { get; set; }
        public List<Category> Categories { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Image> Images { get; set; }
        public List<Order> Orders { get; set; }
    }
}