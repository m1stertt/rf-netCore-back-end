using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.WebAPI.PolicyHandlers;

namespace ScrumMasters.Webshop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController: ControllerBase
    {
    
        private readonly IColorService _colorService;
        
        public ColorController(IColorService colorService)
        {
            _colorService = colorService ?? throw new InvalidDataException("ColorService Cannot Be Null.");
        }
        
        [HttpGet]
        public ActionResult<List<Color>> GetAll()
        {
            return Ok(_colorService.GetColors());
        }
        
        
        [HttpGet("{id:int}")]
        public ActionResult<Color> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to find a color by it's ID in the repository.");
            }
            return Ok(_colorService.GetColorById(id));
        }
        
        [HttpGet("Product/{id:int}")]
        public ActionResult<Color> GetByProductId(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to find a color by it's ID in the repository.");
            }
            return Ok(_colorService.GetColorById(id));
        }
        
        
        [Authorize(Policy = nameof(CanManageColorsHandler))]
        [HttpPost]  
        public ActionResult<Color> Post([FromBody] Color color)
        {
            if (color == null)
            {
                return BadRequest("A color is required before creating a color in the repository.");
            }
            
            return Ok(_colorService.Create(color));
        }
        
        [Authorize(Policy = nameof(CanManageColorsHandler))]
        [HttpPut("{id}")]  
        public ActionResult<Color> Update(int id, [FromBody] Color color)
        {
            if (id < 1 || id != color.Id)
            {
                return BadRequest("Correct id is needed to update a color in the repository.");
            }

            return Ok(_colorService.Update(color));
        }
        
        [Authorize(Policy = nameof(CanManageColorsHandler))]
        [HttpDelete("{id:int}")]
        public ActionResult<Color> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to delete by id.");
            }
            return Ok(_colorService.DeleteById(id));
        }
    }
}