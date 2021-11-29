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
    
    }
}