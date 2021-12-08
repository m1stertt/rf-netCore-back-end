﻿using System.Collections.Generic;

namespace ScrumMasters.Webshop.Core.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Desc { get; set; }
        
        public string Tags { get; set; }

        public string Path { get; set; }
        
        public List<Product> Products { get; set; }
    }
}