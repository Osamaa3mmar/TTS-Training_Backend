using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Interfaces
{
    public interface IOrderService:IGenericService<Order,OrderRequest,OrderResponse>
    {
        public  Task<int> CreateOrderAsync(OrderRequest request);
        public Task<IEnumerable<OrderResponse>> GetAllAsync();
        public Task<OrderFullResponse> GetOrderDetailsById(int id);

    }
}
