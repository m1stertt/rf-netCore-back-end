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
    }
}