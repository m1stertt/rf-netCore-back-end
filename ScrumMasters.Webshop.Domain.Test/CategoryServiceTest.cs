using System.Collections.Generic;
using System.IO;
using Moq;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;
using ScrumMasters.Webshop.Domain.Service;
using Xunit;

namespace ScrumMasters.Webshop.Domain.Test
{
    public class CategoryServiceTest
    {
        private readonly Mock<ICategoryRepository> _mock;
        private readonly CategoryService _service;
        private readonly List<Category> _expected;

        public CategoryServiceTest()
        {
            _mock = new Mock<ICategoryRepository>();
            _service = new CategoryService(_mock.Object);
            _expected = new List<Category>
            {
                new Category {Id = 1, Name = "Category1"},
                new Category {Id = 2, Name = "Category2"}
            };
        }

        [Fact]
        public void CategoriesService_IsICategorieservice()
        {
            Assert.True(_service is ICategoryService);
        }

        [Fact]
        public void CategoriesService_WithNullCategoryRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new CategoryService(null)
            );
        }

        [Fact]
        public void CategoriesService_WithNullCategoryRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new CategoryService(null)
            );
            Assert.Equal("CategoryRepository Cannot Be Null", exception.Message);
        }

        [Fact]
        public void GetCategories_NoFilter_ReturnsListOfAllCategories()
        {
            _mock.Setup(r => r.FindAll())
                .Returns(_expected);
            var actual = _service.GetCategories();
            Assert.Equal(_expected, actual);
        }
    }
}