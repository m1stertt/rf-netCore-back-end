using System.Collections.Generic;

namespace ScrumMasters.Webshop.Core.Models
{
    public class InventoryStock
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public Product Product { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
    }
}