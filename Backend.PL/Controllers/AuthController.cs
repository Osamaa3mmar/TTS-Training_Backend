using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService service;

        public AuthController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await service.Register(request);
            if (result is null)
            {
                return BadRequest("Registration failed. Please check your input and try again.");
            }
            return Ok(new { Id = result.Id, Message = "Registration successful." });
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            LoginResponse res = await service.Login(request);

            if (res.Token == "email")
            {
                return NotFound(new { message = "Email Not Found !" });
            }
            else if (res.Token == "password")
            {
                return NotFound(new { message = "Invalid Password !" });

            }
            else if (res is null)
            {
                return NotFound(new { message = "Error" });
            }

            return Ok(new { Token = res.Token, Message = "Login successful." });
        }
    }
}
