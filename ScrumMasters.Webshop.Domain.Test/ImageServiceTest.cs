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
    public class ImageServiceTest
    {
        private readonly Mock<IImageRepository> _mock;
        private readonly ImageService _service;
        private readonly Image _expected;

        public ImageServiceTest()
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
        public void ImagesService_IsIImageService()
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
        
        [Fact]
        public void CreateImageWithNullCategory_returnsNull()
        {
            Image _expected = null;
            _mock.Setup(r => r.Create(_expected))
                .Returns((Image) null);
            var actual = _service.Create(_expected);
            Assert.Null(actual);
        }
        
        [Fact]
        public void UpdateImageWithNullCategory_returnsNull()
        {
            Image _expected = null;
            _mock.Setup(r => r.Update(_expected))
                .Returns((Image) null);
            var actual = _service.Update(_expected);
            Assert.Null(actual);
        }
        
        [Fact]
        public void GetImageWithIdZero_returnsNull()
        {
            _mock.Setup(r => r.GetById(0))
                .Returns((Image) null);
            var actual = _service.GetById(0);
            Assert.Null(actual);
        }
        
        [Fact]
        public void DeleteImageWithIdZero_returnsNull()
        {
            _mock.Setup(r => r.DeleteById(0))
                .Returns((Image) null);
            var actual = _service.DeleteById(0);
            Assert.Null(actual);
        }
        
        [Fact]
        public void DeleteImageById_ReturnsImage()
        {
            Image _expected = new Image();
            
            _mock.Setup(r => r.DeleteById(1))
                .Returns(_expected);
            var actual = _service.DeleteById(1);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void UpdateImageWithImage_ReturnsImage()
        {
            Image _expected = new Image();
            
            _mock.Setup(r => r.Update(_expected))
                .Returns(_expected);
            var actual = _service.Update(_expected);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void CreateImageWithImage_ReturnsImage()
        {
            Image _expected = new Image();
            
            _mock.Setup(r => r.Create(_expected))
                .Returns(_expected);
            var actual = _service.Create(_expected);
            Assert.Equal(_expected, actual);
        }
    }
}