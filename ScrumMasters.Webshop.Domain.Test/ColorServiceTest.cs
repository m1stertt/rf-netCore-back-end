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
    public class ColorServiceTest
    {
        private readonly Mock<IColorRepository> _mock;
        private readonly ColorService _service;
        private readonly List<Color> _expected;

        public ColorServiceTest()
        {
            _mock = new Mock<IColorRepository>();
            _service = new ColorService(_mock.Object);
            _expected = new List<Color>
            {
                new Color {Id = 1, Title = "Color1"},
                new Color {Id = 2, Title = "Color2"}
            };
        }

        [Fact]
        public void ColorsService_IsIColorservice()
        {
            Assert.True(_service is IColorService);
        }

        [Fact]
        public void ColorsService_WithNullColorRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new ColorService(null)
            );
        }

        [Fact]
        public void ColorsService_WithNullColorRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new ColorService(null)
            );
            Assert.Equal("ColorRepository Cannot Be Null", exception.Message);
        }

        [Fact]
        public void GetColors_NoFilter_ReturnsListOfAllColors()
        {
            _mock.Setup(r => r.FindAll())
                .Returns(_expected);
            var actual = _service.GetColors();
            Assert.Equal(_expected, actual);
        }

        [Fact]
        public void CreateColorWithNullColor_returnsNull()
        {
            Color _expected = null;

            _mock.Setup(r => r.Update(_expected))
                .Returns((Color) null);
            var actual = _service.Update(_expected);
            Assert.Null(actual);
        }

        [Fact]
        public void UpdateColorWithNullColor_returnsNull()
        {
            Color _expected = null;

            _mock.Setup(r => r.Update(_expected))
                .Returns((Color) null);
            var actual = _service.Update(_expected);
            Assert.Null(actual);
        }
        
        [Fact]
        public void DeleteColorByIdWithIdZero_returnsNull()
        {

            _mock.Setup(r => r.DeleteById(0))
                .Returns((Color) null);
            var actual = _service.DeleteById(0);
            Assert.Null(actual);
        }
        
        [Fact]
        public void FindColorByIdWithIdZero_returnsNull()
        {

            _mock.Setup(r => r.FindById(0))
                .Returns((Color) null);
            var actual = _service.DeleteById(0);
            Assert.Null(actual);
        }
        [Fact]
        public void DeleteColorById_ReturnsColor()
        {
            Color _expected = new Color();
            
            _mock.Setup(r => r.DeleteById(1))
                .Returns(_expected);
            var actual = _service.DeleteById(1);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void UpdateColorWithColor_ReturnsColor()
        {
            Color _expected = new Color();
            
            _mock.Setup(r => r.Update(_expected))
                .Returns(_expected);
            var actual = _service.Update(_expected);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void CreateColorWithColor_ReturnsColor()
        {
            Color _expected = new Color();
            
            _mock.Setup(r => r.Create(_expected))
                .Returns(_expected);
            var actual = _service.Create(_expected);
            Assert.Equal(_expected, actual);
        }
    }
}