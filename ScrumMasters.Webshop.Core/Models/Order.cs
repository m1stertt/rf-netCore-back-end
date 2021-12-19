using System.Collections.Generic;

namespace ScrumMasters.Webshop.Core.Models
{
    public class Order
    {
        public int Id { get; set; }

        public List<Product> Products { get; set; }

        }
    }
