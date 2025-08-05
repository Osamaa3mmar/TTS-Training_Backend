using Backend.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Areas.Admin.Payments
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class PaymentsController : ControllerBase
    {
        private readonly  IPaymentsServices paymentsServices;


        public PaymentsController(IPaymentsServices service)
        {
            this.paymentsServices = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPaymentsAsync()
        {
            var payments = await paymentsServices.GetAllAsync();
            return Ok(payments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentByIdAsync(int id)
        {
            var payment = await paymentsServices.GetPaymentDetailesByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetPaymentByOrderIdAsync(int orderId)
        {
            var payment = await paymentsServices.GetPaymentDetailesByOrderIdAsync(orderId);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPatch("ChangePaymentStatus/{id}")]
        public async Task<IActionResult> ChangePaymentStatus([FromRoute] int id) {
            int res = await paymentsServices.TogglePaymentStatusAsync(id);

            if(res == 0)
            {
                return NotFound("Payment not found.");
            }
            
            
            return Ok("Payment status changed successfully.");


        }

    }
}
