using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumMasters.Webshop.Core.Models
{
    public class PagedCategoryProductList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public int CategoryId { get; set; }
        
        public int[] ColorIds { get; set; }
        public PagedCategoryProductList(List<T> items, int count, int pageNumber, int pageSize, int categoryId, int[] colorIds)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            CategoryId = categoryId;
            AddRange(items);
        }
        public static PagedCategoryProductList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize, int categoryId, int[] colorIds)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedCategoryProductList<T>(items, count, pageNumber, pageSize, categoryId, colorIds);
        }
    }
}