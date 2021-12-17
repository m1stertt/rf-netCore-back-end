using System.Collections.Generic;
using System.IO;
using Moq;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;
using ScrumMasters.Webshop.Domain.Service;
using Xunit;

namespace ScrumMasters.Webshop.Domain.Test
{
    public class IProductServiceTest
    {
        public class ProductServiceTest
        {
            private readonly Mock<IProductRepository> _mock;
            private readonly ProductService _service;
            private readonly PagedProductList<Product> _expected;

            public ProductServiceTest()
            {
                _mock = new Mock<IProductRepository>();
                _service = new ProductService(_mock.Object);
                var products = new List<Product>();
                _expected = new PagedProductList<Product>(products,5,1,2, "kjole");
 
            }

            [Fact]
            public void ProductService_IsIProductService()
            {
                Assert.True(_service != null);
            }


            [Fact]
            public void ProductService_WithNullProductRepository_ThrowsInvalidDataException()
            {
                Assert.Throws<InvalidDataException>(
                    () => new ProductService(null)
                );
            }

            [Fact]
            public void ProductService_WithNullProductRepository_ThrowsExceptionWithMessage()
            {
                var exception = Assert.Throws<InvalidDataException>(
                    () => new ProductService(null)
                );
                Assert.Equal("ProductRepository Cannot Be Null", exception.Message);
            }


            [Fact]
            public void GetProducts_CallsProductRepositoriesFindAll_ExactlyOnce()
            {
                var productParameters = new ProductPaginationParameters
                {
                    PageSize = 5,
                    PageNumber = 1
                };

                _service.GetPagedProductList(productParameters);
                _mock.Verify(r => r.GetPagedProductList(productParameters), Times.Once);
            }

            [Fact]
            public void GetProducts_PagedFilter_ReturnsListOfAllProducts()
            {
                var productParameters = new ProductPaginationParameters
                {
                    PageSize = 5,
                    PageNumber = 1
                };
                _mock.Setup(r => r.GetPagedProductList(productParameters))
                    .Returns(_expected);

                var actual = _service.GetPagedProductList(productParameters);
                Assert.Equal(_expected, actual);
            }
            
            [Fact]
            public void GetProduct_WithId_ReturnsProduct()
            {
                Product mock = new Product();
                _mock.Setup(r => r.FindById(1))
                    .Returns(mock);
                var actual = _service.GetProductById(1);
                Assert.Equal(mock, actual);
            }
        
            [Fact]
            public void GetProduct_WithIdZero_ReturnsNull()
            {
                _mock.Setup(r => r.FindById(0))
                    .Returns((Product)null);
                var actual = _service.GetProductById(0);
                Assert.Null(actual);
            }
        
            [Fact]
            public void DeleteProduct_WithIdZero_ReturnsNull()
            {
                _mock.Setup(r => r.DeleteById(0))
                    .Returns((Product)null);
                var actual = _service.DeleteById(0);
                Assert.Null(actual);
            }
        
            [Fact]
            public void CreateProduct_WithProductNull_ReturnsNull()
            {
                Product mock = null;
                _mock.Setup(r => r.Create(mock))
                    .Returns((Product)null);
                var actual = _service.Create(null);
                Assert.Null(actual);
            }
        
            [Fact]
            public void UpdateProduct_WithProductNull_ReturnsNull()
            {
                Product mock = null;
                _mock.Setup(r => r.Create(mock))
                    .Returns((Product)null);
                var actual = _service.Create(null);
                Assert.Null(actual);
            }
            
            [Fact]
            public void DeleteProductById_ReturnsProduct()
            {
                Product _expected = new Product();
            
                _mock.Setup(r => r.DeleteById(1))
                    .Returns(_expected);
                var actual = _service.DeleteById(1);
                Assert.Equal(_expected, actual);
            }
        
            [Fact]
            public void UpdateProductWithProduct_ReturnsProduct()
            {
                Product _expected = new Product();
            
                _mock.Setup(r => r.Update(_expected))
                    .Returns(_expected);
                var actual = _service.Update(_expected);
                Assert.Equal(_expected, actual);
            }
        
            [Fact]
            public void CreateProductWithProduct_ReturnsProduct()
            {
                Product _expected = new Product();
            
                _mock.Setup(r => r.Create(_expected))
                    .Returns(_expected);
                var actual = _service.Create(_expected);
                Assert.Equal(_expected, actual);
            }
        }
    }
}