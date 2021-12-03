using System.Collections.Generic;
using Moq;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
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
        
                [Fact]
                public void GetProducts_WithParam_ReturnsListOfPagedProducts()
                {
                    var mock = new Mock<IProductService>();
                    var products = new List<Product>();
                    var fakeList = new PagedList<Product>(products,5,1,2);
                    var parameters = new ProductParameters();
                    mock.Setup(s => s.GetProducts(parameters))
                        .Returns(fakeList);
                    var service = mock.Object;
                    Assert.Equal(fakeList, service.GetProducts(parameters));
                }
    }
}