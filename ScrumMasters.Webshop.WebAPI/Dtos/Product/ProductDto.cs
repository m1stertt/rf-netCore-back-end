namespace ScrumMasters.Webshop.WebAPI.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        
        public string ProductName { get; set; }
        
        public double ProductPrice { get; set; }
        
        public string ProductDescription { get; set; }
        
        public string ProductImageUrl { get; set; }
    }
}