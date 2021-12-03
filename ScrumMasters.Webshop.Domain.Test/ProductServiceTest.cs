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
    public class IProductServiceTest
    {
        public class ProductServiceTest
        {
            private readonly Mock<IProductRepository> _mock;
            private readonly ProductService _service;
            private readonly PagedList<Product> _expected;

            public ProductServiceTest()
            {
                _mock = new Mock<IProductRepository>();
                _service = new ProductService(_mock.Object);
                var products = new List<Product>();
                _expected = new PagedList<Product>(products,5,1,2);
 
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
                var productParameters = new ProductParameters
                {
                    PageSize = 5,
                    PageNumber = 1
                };

                _service.GetProducts(productParameters);
                _mock.Verify(r => r.GetProducts(productParameters), Times.Once);
            }

            [Fact]
            public void GetProducts_NoFilter_ReturnsListOfAllProducts()
            {
                var productParameters = new ProductParameters
                {
                    PageSize = 5,
                    PageNumber = 1
                };
                _mock.Setup(r => r.GetProducts(productParameters))
                    .Returns(_expected);

                var actual = _service.GetProducts(productParameters);
                Assert.Equal(_expected, actual);
            }
        }
    }
}