using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.WebAPI.PolicyHandlers;

namespace ScrumMasters.Webshop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController: ControllerBase
    {

        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService ?? throw new InvalidDataException("ImageService Cannot Be Null.");
        }

        [HttpGet]
        public ActionResult<List<Image>> GetAll()
        {
            return Ok(_imageService.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Image> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to find a image by it's ID in the repository.");
            }

            return Ok(_imageService.GetById(id));
        }

        [Authorize(Policy = nameof(CanManageProductsHandler))]
        [HttpPost, DisableRequestSizeLimit]  
        public async Task<IActionResult> Post([FromBody] Image image)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                //var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var name = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    if (name != null)
                    {
                        var fileName = name.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        await using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        return Ok(new { dbPath });
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
            return BadRequest();
        }

        [Authorize(Policy = nameof(CanManageProductsHandler))]
        [HttpPut("{id}")]  
        public ActionResult<Image> Update(int id, [FromBody] Image image)
        {
            if (id < 1 || id != image.Id)
            {
                return BadRequest("Correct id is needed to update a image in the repository.");
            }

            return Ok(_imageService.Update(image));
        }

        [Authorize(Policy = nameof(CanManageProductsHandler))]
        [HttpDelete("{id:int}")]
        public ActionResult<Image> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to delete by id.");
            }
            return Ok(_imageService.DeleteById(id));
        }

    }
}