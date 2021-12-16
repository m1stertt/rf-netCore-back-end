using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumMasters.Webshop.Core.Models
{
    public class PagedProductList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        
        public string SearchString { get; private set; }
        public PagedProductList(List<T> items, int count, int pageNumber, int pageSize, string searchString)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SearchString = searchString;
            AddRange(items);
        }
        public static PagedProductList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize, string searchString)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedProductList<T>(items, count, pageNumber, pageSize, searchString);
        }
    }
}