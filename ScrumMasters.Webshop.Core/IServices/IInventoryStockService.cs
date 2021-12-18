using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Core.IServices
{
    public interface IInventoryStockService
    {
        InventoryStock FindById(int id);
        InventoryStock Create(InventoryStock inventoryStock);
        InventoryStock Update(InventoryStock inventoryStock);
        InventoryStock DeleteById(int id);
        List<InventoryStock> FindByProductId(int id);
    }
}