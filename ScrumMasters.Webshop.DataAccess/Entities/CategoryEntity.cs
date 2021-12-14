using System.Collections.Generic;

namespace ScrumMasters.Webshop.DataAccess.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public List<ProductEntity> Product { get; set; }
    }
}