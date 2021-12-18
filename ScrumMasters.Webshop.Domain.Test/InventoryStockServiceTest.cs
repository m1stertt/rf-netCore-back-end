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
    public class InventoryStockServiceTest
    {
        private readonly Mock<IInventoryStockRepository> _mock;
        private readonly InventoryStockService _service;
        private readonly InventoryStock _expected;

        public InventoryStockServiceTest()
        {
            _mock = new Mock<IInventoryStockRepository>();
            _service = new InventoryStockService(_mock.Object);
            _expected = new InventoryStock
            {
                Id = 1,
                Color = new Color{Id = 1},
                Amount = 4,
                Product = new Product{Id = 1}
            };
        }

        [Fact]
        public void InventoryStocksService_IsIInventoryStockService()
        {
            Assert.True(_service is IInventoryStockService);
        }

        [Fact]
        public void InventoryStocksService_WithNullInventoryStockRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new InventoryStockService(null)
            );
        }

        [Fact]
        public void InventoryStocksService_WithNullInventoryStockRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new InventoryStockService(null)
            );
            Assert.Equal("InventoryStockRepository Cannot Be Null", exception.Message);
        }

        [Fact]
        public void GetInventoryStock_WithId_ReturnsInventoryStock()
        {
            _mock.Setup(r => r.FindById(1))
                .Returns(_expected);
            var actual = _service.FindById(1);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void GetInventoryStock_ByProductId_ReturnsInventoryStock()
        {
            List<InventoryStock> invList = new List<InventoryStock>();
            invList.Add(_expected);
            _mock.Setup(r => r.FindByProductId(1))
                .Returns(invList);
            var actual = _service.FindByProductId(1);
            Assert.Equal(invList, actual);
        }
        
        [Fact]
        public void CreateInventoryStockWithNullCategory_returnsNull()
        {
            InventoryStock _expected = null;
            _mock.Setup(r => r.Create(_expected))
                .Returns((InventoryStock) null);
            var actual = _service.Create(_expected);
            Assert.Null(actual);
        }
        
        [Fact]
        public void UpdateInventoryStockWithNullCategory_returnsNull()
        {
            InventoryStock _expected = null;
            _mock.Setup(r => r.Update(_expected))
                .Returns((InventoryStock) null);
            var actual = _service.Update(_expected);
            Assert.Null(actual);
        }
        
        [Fact]
        public void GetInventoryStockWithIdZero_returnsNull()
        {
            _mock.Setup(r => r.FindById(0))
                .Returns((InventoryStock) null);
            var actual = _service.FindById(0);
            Assert.Null(actual);
        }
        
        [Fact]
        public void DeleteInventoryStockWithIdZero_returnsNull()
        {
            _mock.Setup(r => r.DeleteById(0))
                .Returns((InventoryStock) null);
            var actual = _service.DeleteById(0);
            Assert.Null(actual);
        }
        
        [Fact]
        public void DeleteInventoryStockById_ReturnsInventoryStock()
        {
            //InventoryStock _expected = new InventoryStock();
            
            _mock.Setup(r => r.DeleteById(1))
                .Returns(_expected);
            var actual = _service.DeleteById(1);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void UpdateInventoryStockWithInventoryStock_ReturnsInventoryStock()
        {
            //InventoryStock _expected = new InventoryStock();
            
            _mock.Setup(r => r.Update(_expected))
                .Returns(_expected);
            var actual = _service.Update(_expected);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void CreateInventoryStockWithInventoryStock_ReturnsInventoryStock()
        {
            //InventoryStock _expected = new InventoryStock();
            
            _mock.Setup(r => r.Create(_expected))
                .Returns(_expected);
            var actual = _service.Create(_expected);
            Assert.Equal(_expected, actual);
        }
    }
}