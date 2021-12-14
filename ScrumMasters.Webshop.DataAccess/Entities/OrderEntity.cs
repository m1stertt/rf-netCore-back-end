using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.DataAccess.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public List<ProductEntity> Products { get; set; }
        
        
    }
}