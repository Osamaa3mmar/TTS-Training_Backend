using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Areas.User.Orders
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("User")]
    [Authorize(Roles ="User")]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService service;
        public OrdersController(IOrderService service)
        {
            this.service = service;
        }

        //تاكد انو الي بعت الريكويست هو صاحب الايدي 
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            var userId=User.FindFirst("Id")?.Value;
            if(userId != request.AppUserId)
            {
                return BadRequest(new { message = "You are not authorized to create this order." });
            }
            var result = await service.CreateOrderAsync(request);


            return Ok(result);

        }
        [HttpGet()]
        public async Task<IActionResult> GetOrdersByUserId()
        {
            var userId = User.FindFirst("Id")?.Value;
            var orders = await service.GetOrdersByUserIdAsync(userId);


            return Ok(new { message="Success",orders});
        }

        [HttpGet("Full/{id}")]
        public async Task<IActionResult> GetOrderFullDetailesById([FromRoute] int id)
        {
            var order = await service.GetOrderDetailsById(id);


            return Ok(order);

        }
        [HttpPatch("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute]int id, [FromBody] ChangeOrderStatusRequest req)
        {
           int res= await service.ChangeOrderStatusAsync(id, req.Status);
            if (res == 0)
            {
                return BadRequest(new { message = "Order not found or status change failed." });
            }
            return Ok(new { message = "Order status changed successfully." });
        }

    }
}
