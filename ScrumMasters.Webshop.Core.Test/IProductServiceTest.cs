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
                public void GetProducts_WithNoParam_ReturnsListOfAllProducts()
                {
                    var mock = new Mock<IProductService>();
                    var fakeList = new List<Product>();
                    mock.Setup(s => s.GetProducts())
                        .Returns(fakeList);
                    var service = mock.Object;
                    Assert.Equal(fakeList, service.GetProducts());
                }
    }
}