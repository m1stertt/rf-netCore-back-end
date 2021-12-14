namespace ScrumMasters.Webshop.DataAccess.Entities
{
    public class InventoryStockEntity
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public ProductEntity Product { get; set; }
        public ColorEntity Color { get; set; }
        public SizeEntity Size { get; set; }
        
        
    }
}