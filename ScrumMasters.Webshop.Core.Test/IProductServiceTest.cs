using System.Collections.Generic;
using System.IO;
using Moq;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;
using ScrumMasters.Webshop.Domain.Service;
using Xunit;

namespace ScrumMasters.Webshop.Core.Test
{
    public class IProductServiceTest
    {
                [Fact]
                public void IProductService_IsAvailable()
                {
                    var service = new Mock<IProductService>().Object;
                    Assert.NotNull(service);
                }
        
            private readonly Mock<IProductRepository> _mock;
            private readonly ProductService _service;
            private readonly PagedProductList<Product> _expected;

            public IProductServiceTest()
            {
                _mock = new Mock<IProductRepository>();
                _service = new ProductService(_mock.Object);
                var products = new List<Product>();
                _expected = new PagedProductList<Product>(products,5,1,2, "kjoler");
 
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
    }
}