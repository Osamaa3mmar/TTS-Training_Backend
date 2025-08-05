using Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository.Interfaces
{
    public interface IOrderRepository:IGenericRepository<Order>
    {

        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetOrderDetailsById(int id);
        Task<IEnumerable<Order>> GetOrdersByUserId(string id);
    }
}
