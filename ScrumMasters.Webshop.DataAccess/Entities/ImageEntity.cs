namespace ScrumMasters.Webshop.DataAccess.Entities
{
    public class ImageEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Desc { get; set; }
        
        public string Tags { get; set; }

        public string Path { get; set; }
    }
}