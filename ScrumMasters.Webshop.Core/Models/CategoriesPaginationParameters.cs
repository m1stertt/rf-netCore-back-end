namespace ScrumMasters.Webshop.Core.Models
{
    public class CategoriesPaginationParameters
    {
        const int _maxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }

        public int categoryId { get; set; }
    }
}