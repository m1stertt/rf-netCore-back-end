namespace ScrumMasters.Webshop.DataAccess.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        
        public string ProductName { get; set; }
        
        public double ProductPrice { get; set; }
        
        public string ProductDescription { get; set; }
        
        public string ProductImageUrl { get; set; }
    }
}