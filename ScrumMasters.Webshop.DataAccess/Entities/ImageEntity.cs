using System.Collections.Generic;

namespace ScrumMasters.Webshop.DataAccess.Entities
{
    public class ImageEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Path { get; set; }
        
        public ProductEntity Product { get; set; }
    }
}