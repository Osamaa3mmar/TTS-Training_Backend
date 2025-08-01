using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService service;
        private readonly FilesUtiles file;

        public ProductsController(IProductService service,FilesUtiles file)
        {
            this.file = file;
            this.service = service; 
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute]int id)
        {
            var product = await service.GetByIdAsync(id);

            if(product == null)
            {
                return NotFound($"There Is No Product With {id} Id !");
            }

            return Ok(new { message="Success",product});
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllCategory()
        {
            var products = await service.GetAllAsync();
            return Ok(new { message="Success",products });
        }
        [HttpPost()]
        public async Task<IActionResult> CreateProduct([FromForm]ProductFileRequest request)
        {
            var imageUrl=await file.UploadImage(request.Image);
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

            int res= await service.AddAsync(productRequest);
            if (res == 0)
            {
            return BadRequest("Product Could Not Be Created !");
            }

            return Ok(new { message = "Success" });
        }


    }
}
