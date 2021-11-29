using System.Collections.Generic;
using Moq;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using Xunit;

namespace ScrumMasters.Webshop.Core.Test
{
    public class CategoryServiceTest
    {
        [Fact]
        public void ICategoryService_IsAvailable()
        {
            var service = new Mock<ICategoryService>().Object;
            Assert.NotNull(service);
        }
        
        [Fact]
        public void GetCategory_WithNoParam_ReturnsListOfAllCategory()
        {
            var mock = new Mock<ICategoryService>();
            var fakeList = new List<Category>();
            mock.Setup(s => s.GetCategories())
                .Returns(fakeList);
            var service = mock.Object;
            Assert.Equal(fakeList, service.GetCategories());
        }
    }
}