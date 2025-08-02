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
    public class OrderRepository:GenericRepository<Order>,IOrderRepository
    {
        private readonly ApplicationDbContext context;
        public OrderRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await context.Orders.Include(o => o.AppUser).ToListAsync();   
        }
        public Task<Order> GetOrderDetailsById(int id)
        {
            return context.Orders.Include(o => o.AppUser).Include(o => o.OrderDetails).ThenInclude(d=>d.Product).FirstOrDefaultAsync(o=> o.Id==id);
        }


    }
}
