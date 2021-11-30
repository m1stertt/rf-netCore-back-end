using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
    
        private readonly ICategoryService _categoryService;
            
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new InvalidDataException("ProductService Cannot Be Null.");
        }
            
        [HttpGet]
        public ActionResult<List<Category>> GetAll()
        {
            return Ok(_categoryService.GetCategories());
        }
        
        [HttpGet("{id:int}")]
        public ActionResult<Category> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to find by id.");
            }
            return Ok(_categoryService.GetCategoryById(id));
        }
        
        [HttpPost]  
        public ActionResult<Category> Post([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("A category is required to create a category in the repository");
            }

            return Ok(_categoryService.Create(category));
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult<Category> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to delete by id.");
            }
            return Ok(_categoryService.DeleteById(id));
        }
        
        [HttpPut("{id}")]  
        public ActionResult<Category> Update(int id, [FromBody] Category category)
        {
            if (id < 1 || id != category.Id)
            {
                return BadRequest("Correct id is needed to update a product.");
            }
            
            return Ok(_categoryService.Update(category));
        }
    
    }
}