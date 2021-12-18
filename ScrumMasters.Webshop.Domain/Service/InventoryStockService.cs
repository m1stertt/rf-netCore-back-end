using System.Collections.Generic;
using System.IO;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.Domain.Service
{
    public class InventoryStockService : IInventoryStockService
    {
        private readonly IInventoryStockRepository _inventoryStockRepository;

        public InventoryStockService(IInventoryStockRepository inventoryStockRepository)
        {
            _inventoryStockRepository = inventoryStockRepository ?? throw new InvalidDataException("InventoryStockRepository Cannot Be Null");
        }

        public InventoryStock FindById(int id)
        {
            return _inventoryStockRepository.FindById(id);
        }

        public InventoryStock Create(InventoryStock inventoryStock)
        {
            return _inventoryStockRepository.Create(inventoryStock);
        }

        public InventoryStock Update(InventoryStock inventoryStock)
        {
            return _inventoryStockRepository.Update(inventoryStock);
        }

        public InventoryStock DeleteById(int id)
        {
            return _inventoryStockRepository.DeleteById(id);
        }

        public List<InventoryStock> FindByProductId(int id)
        {
            return _inventoryStockRepository.FindByProductId(id);
        }
    }
}