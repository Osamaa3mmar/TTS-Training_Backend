using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.Models;
using Backend.DAL.Repository.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService service;
        public CategoriesController(ICategoryService service)
        {
            this.service = service;
        }


       

        [HttpGet("All")]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await service.GetAllAsync(true);

            var response = new
            {
                message = "Success",
                categories
            };
            return Ok(response);
        }

       

      


       

       

    }
}
