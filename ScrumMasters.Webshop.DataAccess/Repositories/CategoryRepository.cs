using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MainDbContext context;
        
        public CategoryRepository(MainDbContext ctx)
        {
            context = ctx ?? throw new InvalidDataException("Category Repository Must have a DBContext");
        }
        public List<Category> FindAll()
        {
            return context.Categories
                .Select(pe => new Category
                {
                    Id = pe.Id,
                    Title = pe.Title,
                    URLString = pe.URLString
                })
                .ToList();
        }
    }
}