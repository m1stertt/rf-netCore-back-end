using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.WebAPI.PolicyHandlers;

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
        public ActionResult<List<Product>> GetAll([FromQuery] ProductParameters productParameters)
        {
            var products = _productService.GetProducts(productParameters);
            
            var metadata = new 
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious,
                products.SearchString
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(products);
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

        [Authorize(Policy = nameof(CanManageProductsHandler))]
        [HttpPost]  
        public ActionResult<Product> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("A product is required before creating a product in the repository.");
            }
                
            return Ok(_productService.Create(product));
        }

        [Authorize(Policy = nameof(CanManageProductsHandler))]
        [HttpPut("{id}")]  
        public ActionResult<Product> Update(int id, [FromBody] Product product)
        {
            if (id < 1 || id != product.Id)
            {
                return BadRequest("Correct id is needed to update a product in the repository.");
            }

            return Ok(_productService.Update(product));
        }

        [Authorize(Policy = nameof(CanManageProductsHandler))]
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