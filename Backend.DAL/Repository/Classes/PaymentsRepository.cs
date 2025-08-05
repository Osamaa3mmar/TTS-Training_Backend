using Backend.DAL.Data;
using Backend.DAL.Models;
using Backend.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository.Classes
{
    public class PaymentsRepository:GenericRepository<Payment>,IPaymentRepository
    {
        private readonly ApplicationDbContext context;
        public PaymentsRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Payment> GetPaymentDetailesByIdAsync(int id)
        {
            var payment = await context.Payments.Include(p => p.Order).ThenInclude(o => o.AppUser).FirstOrDefaultAsync(p => p.Id == id);
                if(payment == null)
            {
                return null;
            }
                return payment;
        }

        public async Task<Payment> GetPaymentDetailesByOrderIdAsync(int orderId)
        {
            var payment = await context.Payments.Include(p => p.Order).ThenInclude(o => o.AppUser).FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (payment == null)
            {
                return null;
            }
            return payment;
        }
    }
}
