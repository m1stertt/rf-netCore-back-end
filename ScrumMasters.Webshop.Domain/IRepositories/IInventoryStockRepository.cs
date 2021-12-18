using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Domain.IRepositories
{
    public interface IInventoryStockRepository
    {
        List<InventoryStock> FindAll();
        InventoryStock FindById(int id);
        InventoryStock Create(InventoryStock inventoryStock);
        InventoryStock Update(InventoryStock inventoryStock);
        InventoryStock DeleteById(int id);
        List<InventoryStock> FindByProduct(Product product);
    }
}