using System.Collections.Generic;
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
    public class SizeController: ControllerBase
    {
    
        private readonly ISizeService _sizeService;
        
        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService ?? throw new InvalidDataException("SizeService Cannot Be Null.");
        }
        
        [HttpGet]
        public ActionResult<List<Size>> GetAll()
        {
            return Ok(_sizeService.GetSizes());
        }
        
        
        [HttpGet("{id:int}")]
        public ActionResult<Size> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to find a size by it's ID in the repository.");
            }
            return Ok(_sizeService.GetSizeById(id));
        }
        
        
        [Authorize(Policy = nameof(CanManageSizesHandler))]
        [HttpPost]  
        public ActionResult<Size> Post([FromBody] Size size)
        {
            if (size == null)
            {
                return BadRequest("A size is required before creating a size in the repository.");
            }
            
            return Ok(_sizeService.Create(size));
        }
        
        //[Authorize(Policy = nameof(CanManageSizesHandler))]
        [HttpPut("{id}")]  
        public ActionResult<Size> Update(int id, [FromBody] Size size)
        {
            if (id < 1 || id != size.Id)
            {
                return BadRequest("Correct id is needed to update a size in the repository.");
            }

            return Ok(_sizeService.Update(size));
        }
        
        [Authorize(Policy = nameof(CanManageSizesHandler))]
        [HttpDelete("{id:int}")]
        public ActionResult<Size> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to delete by id.");
            }
            return Ok(_sizeService.DeleteById(id));
        }
    }
}