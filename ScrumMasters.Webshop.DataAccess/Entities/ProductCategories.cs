namespace ScrumMasters.Webshop.DataAccess.Entities
{
    public class ProductCategories
    {
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}