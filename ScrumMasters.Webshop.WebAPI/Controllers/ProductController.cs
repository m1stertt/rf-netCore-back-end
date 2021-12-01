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
    public class ProductController : ControllerBase
    {
    
            private readonly IProductService _productService;
            
            public ProductController(IProductService productService)
            {
                _productService = productService ?? throw new InvalidDataException("ProductService Cannot Be Null.");
            }
            
            [HttpGet]
            public ActionResult<List<Product>> GetAll()
            {
                return Ok(_productService.GetProducts());
            }
            
            
            [HttpGet("{id:int}")]
            public ActionResult<Product> GetById(int id)
            {
                if (id == 0)
                {
                    return BadRequest("An ID is required to find a product by it's ID in the repository.");
                }
                return Ok(_productService.GetProductById(id));
            }
            
            [HttpPost]  
            public ActionResult<Product> Post([FromBody] Product product)
            {
                if (product == null)
                {
                    return BadRequest("A product is required before creating a product in the repository.");
                }
                
                return Ok(_productService.Create(product));
            }
            
            [HttpPut("{id}")]  
            public ActionResult<Product> Update(int id, [FromBody] Product product)
            {
                if (id < 1 || id != product.Id)
                {
                    return BadRequest("Correct id is needed to update a product in the repository.");
                }

                return Ok(_productService.Update(product));
            }
            
            [HttpDelete("{id:int}")]
            public ActionResult<Product> DeleteById(int id)
            {
                if (id == 0)
                {
                    return BadRequest("An ID is required to delete by id.");
                }
                return Ok(_productService.DeleteById(id));
            }
    
    }
}