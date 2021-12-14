using System.Collections.Generic;

namespace ScrumMasters.Webshop.DataAccess.Entities
{
    public class ColorEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public List<ProductEntity> Products { get; set; }
        public string ColorString { get; set; }
    }
}