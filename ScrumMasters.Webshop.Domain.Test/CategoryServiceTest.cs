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
        
                [Fact]
        public void GetCategoryById_ReturnsCategory()
        {
            
            var _expected = new Category();
            _mock.Setup(r => r.GetById(1))
                .Returns(_expected);
            var actual = _service.GetCategoryById(1);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void GetCategoryByIdWithZero_ReturnsNull()
        {
            _mock.Setup(r => r.GetById(0))
                .Returns((Category) null);
            var actual = _service.GetCategoryById(0);
            Assert.Null(actual);
        }
        
        [Fact]
        public void DeleteCategoryByIdWithZero_returnsNull()
        {
            _mock.Setup(r => r.DeleteById(0))
                .Returns((Category) null);
            var actual = _service.DeleteById(0);
            Assert.Null(actual);
        }
        [Fact]
        public void UpdateCategoryWithNullCategory_returnsNull()
        {
            Category _expected = null;
            
            _mock.Setup(r => r.Update(_expected))
                .Returns((Category) null);
            var actual = _service.Update(_expected);
            Assert.Null(actual);
        }
        
        [Fact]
        public void CreateCategoryWithNullCategory_returnsNull()
        {
            Category _expected = null;
            
            _mock.Setup(r => r.Create(_expected))
                .Returns((Category) null);
            var actual = _service.Create(_expected);
            Assert.Null(actual);
        }
        
        [Fact]
        public void DeleteCategoryById_ReturnsCategory()
        {
            Category _expected = new Category();
            
            _mock.Setup(r => r.DeleteById(1))
                .Returns(_expected);
            var actual = _service.DeleteById(1);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void UpdateCategoryWithCategory_ReturnsCategory()
        {
            Category _expected = new Category();
            
            _mock.Setup(r => r.Update(_expected))
                .Returns(_expected);
            var actual = _service.Update(_expected);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void CreateCategoryWithCategory_ReturnsCategory()
        {
            Category _expected = new Category();
            
            _mock.Setup(r => r.Create(_expected))
                .Returns(_expected);
            var actual = _service.Create(_expected);
            Assert.Equal(_expected, actual);
        }
    }
    }
    

    
    
