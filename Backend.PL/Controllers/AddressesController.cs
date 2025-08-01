using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService service;
        public AddressesController(IAddressService service)
        {
            this.service = service;
        }

        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetAddressByUserId([FromRoute] string id)
        {
            var address = await service.GetByUserIdAsync(id);

            if(address is null)
            {
                return NotFound($"No User With {id} Id !");
            }
            return Ok(new { message = "Success", address });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, [FromBody] AddressRequest request)
        {
            int res = await service.UpdateAsync(id,request);
            if (res == 0)
            {
                return NotFound($"No Address With {id} Id !");
            }
            var address = await service.GetByIdAsync(id);
            return Ok(new { message = "Success",address });
        }
    }
}
