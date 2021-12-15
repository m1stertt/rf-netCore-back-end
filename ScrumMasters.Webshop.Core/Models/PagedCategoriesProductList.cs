using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumMasters.Webshop.Core.Models
{
    public class PagedCategoriesProductList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public int CategoryId { get; set; }
        public PagedCategoriesProductList(List<T> items, int count, int pageNumber, int pageSize, int categoryId)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            CategoryId = categoryId;
            AddRange(items);
        }
        public static PagedCategoriesProductList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize, int categoryId)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedCategoriesProductList<T>(items, count, pageNumber, pageSize, categoryId);
        }
    }
}