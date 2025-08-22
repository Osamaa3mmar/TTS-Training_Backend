using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Areas.Admin.Orders
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class OrdersController : ControllerBase
    {


        private readonly IOrderService service;
        public OrdersController(IOrderService service)
        {
            this.service = service;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var result = await service.GetAllAsync();
            return Ok(result);
        }

        [HttpPatch("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute]int id, [FromBody]ChangeOrderStatusRequest req)
        {
            int res =await service.ChangeOrderStatusAsync(id, req.Status);
            if (res == 0)
            {
                return BadRequest(new { message = "Failed to change order status." });
            }
            return Ok(new { message = "Order status changed successfully." });
        }

        [HttpGet("Full/{id}")]
        public async Task<IActionResult> GetOrderFullDetailesById([FromRoute] int id)
        {
            var order = await service.GetOrderDetailsById(id);


            return Ok(order);

        }




    }
}
