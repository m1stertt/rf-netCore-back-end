using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.WebAPI.PolicyHandlers;

namespace ScrumMasters.Webshop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryStockController : ControllerBase
    {
        private readonly IInventoryStockService _inventoryStockService;
        
        public InventoryStockController(IInventoryStockService inventoryStockService)
        {
            _inventoryStockService = inventoryStockService ?? throw new InvalidDataException("InventoryStockService Cannot Be Null.");
        }
        
        [Authorize(Policy = nameof(CanManageProductsHandler))]
        [HttpPost]  
        public ActionResult<InventoryStock> Post([FromBody] InventoryStock invStock)
        {
            if (invStock == null)
            {
                return BadRequest("A inventory stock is required before creating a inventory stock in the repository.");
            }
            
            return Ok(_inventoryStockService.Create(invStock));
        }
        
        [HttpGet("{id:int}")]
        public ActionResult<InventoryStock> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to find a inventory stock by it's ID in the repository.");
            }
            return Ok(_inventoryStockService.FindById(id));
        }
        
        [HttpGet("Product/{id:int}")]
        public ActionResult<InventoryStock> GetByProductId(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to find a inventory stock by it's ID in the repository.");
            }
            return Ok(_inventoryStockService.FindByProductId(id));
        }

        [Authorize(Policy = nameof(CanManageProductsHandler))]
        [HttpPut("{id}")]  
        public ActionResult<InventoryStock> Update(int id, [FromBody] InventoryStock invStock)
        {
            if (id < 1 || id != invStock.Id)
            {
                return BadRequest("Correct id is needed to update a inventory stock in the repository.");
            }

            return Ok(_inventoryStockService.Update(invStock));
        }
        
        [Authorize(Policy = nameof(CanManageProductsHandler))]
        [HttpDelete("{id:int}")]
        public ActionResult<InventoryStock> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to delete by id.");
            }
            return Ok(_inventoryStockService.DeleteById(id));
        }
    }
}