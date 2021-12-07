﻿namespace ScrumMasters.Webshop.Core.Models
{
    public class ProductParameters
    {
        const int _maxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
            }
        }

        public string Search { get; set; }
    }
}