using System.Collections.Generic;
using System.IO;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.Domain.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new InvalidDataException("CategoryRepository Cannot Be Null");
        }
        public List<Category> GetCategories()
        {
            return _categoryRepository.FindAll();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public Category DeleteById(int id)
        {
            return _categoryRepository.DeleteById(id);
        }

        public Category Update(Category category)
        {
            return _categoryRepository.Update(category);
        }

        public Category Create(Category category)
        {
            return _categoryRepository.Create(category);
        }

        public List<Product> GetProductsByCategoryId(int id)
        {
            return _categoryRepository.GetProductsByCategoryId(id);
        }
    }
}