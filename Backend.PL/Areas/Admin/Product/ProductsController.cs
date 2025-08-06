using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Areas.Admin.Product
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService service;
        private readonly FilesUtiles file;
        private readonly ICategoryService categoryService;

        public ProductsController(IProductService service,ICategoryService categoryService, FilesUtiles file)
        {
            this.file = file;
            this.service = service;
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductById([FromQuery] int id)
        {
            var product = await service.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound($"There Is No Product With {id} Id !");
            }
            return Ok(new { message = "Success", product });
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductFileRequest request)
        {

            var cat=await categoryService.GetByIdAsync(request.CategoryId);
            if(cat == null)
            {
                return BadRequest("Category Not Found !");
            }
            if(request.Image == null)
            {
                return BadRequest("Image Is Required !");
            }
            var imageUrl = await file.UploadImage(request.Image);
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest();
            }
            var productRequest = new ProductRequest
            {
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                CategoryId = request.CategoryId,
                ImageUrl = imageUrl,
                Description = request.Description
            };

            int res = await service.AddAsync(productRequest);
            if (res == 0)
            {
                return BadRequest("Product Could Not Be Created !");
            }

            return CreatedAtAction(nameof(GetProductById),new {Message="Created Success !" });
        }



        [HttpPatch("{id}/SetQuantity")]
        public async Task<IActionResult> SetQuantity([FromBody] QuantityChangeRequest quantity, [FromRoute] int id)
        {
            var res = await service.ChangeQuatityById(id, quantity.Quantity);
            if (!res)
            {
                return BadRequest("Quantity Could Not Be Changed !");
            }
            return Ok(new { message = "Quantity Changed Successfully !" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {

            var result = await service.DeleteAsync(id);
            if (result == 0)
            {
                return NotFound($"There Is No Product With {id} Id !");
            }


            return Ok(new { message = "Deleted !" });
        }


        [HttpGet("All")]
        public async Task<IActionResult> GetAllCategory()
        {
            var products = await service.GetAllAsync(false);
            return Ok(new { message = "Success", products });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromForm]ProductFileRequest request)
        {
            string imageUrl = "";
            if (request.Image == null)
            {
            imageUrl=(await service.GetByIdAsync(id)).ImageUrl;

            }
            else
            {
                imageUrl=await file.UploadImage(request.Image);
            }
            var newProduct = new ProductRequest()
            {
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                CategoryId = request.CategoryId,
                Description = request.Description,
                ImageUrl = imageUrl
            };
            int res = await service.UpdateAsync(id, newProduct);
            if (res == 0)
            {
                return BadRequest("Product Could Not Be Updated !");
            }
            var product = await service.GetByIdAsync(id);
            return Ok(new { message = "Product Updated Successfully !",product });

        }






    }
}
