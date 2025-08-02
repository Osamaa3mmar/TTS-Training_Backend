using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService service;
        public OrdersController(IOrderService service)
        {
        this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]OrderRequest request) {

            var result = await service.CreateOrderAsync(request);


            return Ok(result);
        
        }

        [HttpGet()]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await service.GetAllAsync();


            return Ok(orders);
        }


        [HttpGet("Full/{id}")]
        public async Task<IActionResult> GetOrderFullDetailesById([FromRoute]int id)
        {
            var order = await service.GetOrderDetailsById(id);


            return Ok(order);

        }
    
    }

}
