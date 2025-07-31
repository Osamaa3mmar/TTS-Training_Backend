using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.Models;
using Backend.DAL.Repository.Classes;
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


        [HttpGet("Full/{id}")]
        public async Task<IActionResult> GetFullCategory([FromRoute] int id)
        {
            var category = await service.GetFullCategoryAsync(id);
            if (category == null)
            {
                return NotFound(new
                {
                    message = "Category not found"
                });
            }
            return Ok(new
            {
                message = "Category found",
                category
            });
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await service.GetAllAsync();

            var response = new
            {
                message = "Success",
                categories
            };
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            int numRowsAffected=await service.AddAsync(request);

            if (numRowsAffected==0)
            {
                return BadRequest(new
                {
                    message = "Failed to create category"
                });
            }
            var response = new
            {
                message = "Category Created Successfully !",
            };
            return Ok(response); 
        }
       

        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryRequest request, [FromRoute] int id) {
            int numRowsAffected = await service.UpdateAsync(id, request);
            if(numRowsAffected == 0)
            {
                return NotFound(new
                {
                    message = "Category not found or could not be updated"
                });
            }

            return Ok(new
            {
                message = "Category Updated Successfully !"
            });
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            int numRowsAffected = await service.DeleteAsync(id);
            if (numRowsAffected == 0)
            {
                return NotFound(new
                {
                    message = "Category not found or could not be deleted"
                });
            }
            return Ok(new
            {
                message = "Category Deleted Successfully !"
            });
        }

        [HttpDelete("Delete/All")]
        public async Task<IActionResult> DeleteAllCategories()
        {
            int x= await service.DeleteAllAsync();
            if(x == 0)
            {
                return NotFound(new
                {
                    message = "No categories found to delete"
                });
            }
            return Ok(new
            {
                message = "All categories deleted successfully!"
            });
        }
       

    }
}
