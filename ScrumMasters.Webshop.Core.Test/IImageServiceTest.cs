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
    public class IImageServiceTest
    {
        [Fact]
        public void IImageService_IsAvailable()
        {
            var service = new Mock<IImageService>().Object;
            Assert.NotNull(service);
        }
        
        private readonly Mock<IImageRepository> _mock;
        private readonly ImageService _service;
        private readonly Image _expected;

        public IImageServiceTest()
        {
            _mock = new Mock<IImageRepository>();
            _service = new ImageService(_mock.Object);
            _expected = new Image
            {
                Id = 1,
                Title = "Image1"

            };
        }

        [Fact]
        public void ImagesService_IsIImageservice()
        {
            Assert.True(_service is IImageService);
        }

        [Fact]
        public void ImagesService_WithNullImageRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new ImageService(null)
            );
        }

        [Fact]
        public void ImagesService_WithNullImageRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new ImageService(null)
            );
            Assert.Equal("ImageRepository Cannot Be Null", exception.Message);
        }

        [Fact]
        public void GetImage_WithId_ReturnsImage()
        {
            _mock.Setup(r => r.GetById(1))
                .Returns(_expected);
            var actual = _service.GetById(1);
            Assert.Equal(_expected, actual);
        }
    }
}