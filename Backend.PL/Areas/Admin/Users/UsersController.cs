using Backend.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Areas.Admin.Users
{
    [Route("api/[area]/[controller]")]
    [ApiController]

    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UsersController : ControllerBase
    {
            
        private readonly IUserService service;
        public UsersController(IUserService service)
        {
            this.service = service;
        }



        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var users = await service.getAllWithAddress();
            if (users == null) {
                return NotFound(new { message = "Error" });
            }

            return Ok(new { users,message="Success" });
        }





    }
}
