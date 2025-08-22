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
            var product = await service.GetByIdWithCategoryAsync(id);

            if(product == null)
            {
                return NotFound($"There Is No Product With {id} Id !");
            }

            return Ok(new { message="Success",product});
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllCategory()
        {
            var products = await service.GetAllAsync(true);
            return Ok(new { message="Success",products });
        }

       

       

       


    }
}
