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
    public class SizeServiceTest
    {
        private readonly Mock<ISizeRepository> _mock;
        private readonly SizeService _service;
        private readonly List<Size> _expected;

        public SizeServiceTest()
        {
            _mock = new Mock<ISizeRepository>();
            _service = new SizeService(_mock.Object);
            _expected = new List<Size>
            {
                new Size {Id = 1, Title = "Size1"},
                new Size {Id = 2, Title = "Size2"}
            };
        }

        [Fact]
        public void SizeService_IsISizeervice()
        {
            Assert.True(_service is ISizeService);
        }

        [Fact]
        public void SizeService_WithNullSizeRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new SizeService(null)
            );
        }

        [Fact]
        public void SizeService_WithNullSizeRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new SizeService(null)
            );
            Assert.Equal("SizeRepository Cannot Be Null", exception.Message);
        }

        [Fact]
        public void GetSize_NoFilter_ReturnsListOfAllSize()
        {
            _mock.Setup(r => r.FindAll())
                .Returns(_expected);
            var actual = _service.GetSizes();
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void GetSize_WithId_ReturnsSize()
        {
            Size _expected = new Size();
            _mock.Setup(r => r.FindById(1))
                .Returns(_expected);
            var actual = _service.GetSizeById(1);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void GetSize_WithIdZero_ReturnsNull()
        {
            _mock.Setup(r => r.FindById(0))
                .Returns((Size)null);
            var actual = _service.GetSizeById(0);
            Assert.Null(actual);
        }
        
        [Fact]
        public void DeleteSize_WithIdZero_ReturnsNull()
        {
            _mock.Setup(r => r.DeleteById(0))
                .Returns((Size)null);
            var actual = _service.DeleteById(0);
            Assert.Null(actual);
        }
        
        [Fact]
        public void CreateSize_WithSizeNull_ReturnsNull()
        {
            Size mock = null;
            _mock.Setup(r => r.Create(mock))
                .Returns((Size)null);
            var actual = _service.Create(null);
            Assert.Null(actual);
        }
        
        [Fact]
        public void UpdateSize_WithSizeNull_ReturnsNull()
        {
            Size mock = null;
            _mock.Setup(r => r.Create(mock))
                .Returns((Size)null);
            var actual = _service.Create(null);
            Assert.Null(actual);
        }
        [Fact]
        public void DeleteSizeById_ReturnsSize()
        {
            Size _expected = new Size();
            
            _mock.Setup(r => r.DeleteById(1))
                .Returns(_expected);
            var actual = _service.DeleteById(1);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void UpdateSizeWithSize_ReturnsSize()
        {
            Size _expected = new Size();
            
            _mock.Setup(r => r.Update(_expected))
                .Returns(_expected);
            var actual = _service.Update(_expected);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void CreateSizeWithSize_ReturnsSize()
        {
            Size _expected = new Size();
            
            _mock.Setup(r => r.Create(_expected))
                .Returns(_expected);
            var actual = _service.Create(_expected);
            Assert.Equal(_expected, actual);
        }
    }
}