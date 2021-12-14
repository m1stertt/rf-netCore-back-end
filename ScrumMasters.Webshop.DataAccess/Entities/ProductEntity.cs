using System;
using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.DataAccess.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        
        public string ProductName { get; set; }
        
        public double ProductPrice { get; set; }
        
        public string ProductDescription { get; set; }
        
        public string ProductImageUrl { get; set; }

        public bool ProductFeatured { get; set; }
        
        public List<CategoryEntity> Categories { get; set; }
        public List<ColorEntity> Colors { get; set; }
        public List<SizeEntity> Sizes { get; set; }
        public List<ImageEntity> Images { get; set; }
        
        public List<OrderEntity> Orders { get; set; }
    }
}