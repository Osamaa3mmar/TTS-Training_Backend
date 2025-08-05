using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using Backend.DAL.Repository.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Classes
{
    public class OrderService:GenericService<Order,OrderRequest,OrderResponse>,IOrderService
    {
        private readonly IOrderRepository repo;
        private readonly IProductRepository productRepo;

        public OrderService(IOrderRepository repo, IProductRepository productRepo) : base(repo)
        {
            this.repo = repo;
            this.productRepo = productRepo;
        }

        public async Task<int> ChangeOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await repo.GetByIdAsync(id);
            order.Status =status;
            return await repo.UpdateAsync(order);
        }

        public async Task<int> CreateOrderAsync(OrderRequest request)
        {
            using var transaction = await repo.StartTransAction();

            var order = new Order()
            {
                AppUserId = request.AppUserId,
                OrderDetails = request.OrderDetails.Adapt<ICollection<OrderDetails>>(),
                Payment = request.Payment.Adapt<Payment>()
            };
            decimal totalAmount = 0;
            foreach (var x in order.OrderDetails)
            {
                var product = await productRepo.GetByIdAsync(x.ProductId);
                if(!(product.Quantity>= x.Quantity))
                {
                    await transaction.RollbackAsync();
                    return 0;
                }
                product.Quantity -= x.Quantity;
                totalAmount += x.Quantity * product.Price;
                x.UnitPrice = product.Price;
                if (product.Quantity == 0)
                {
                    product.Status = Status.InActive;
                }
            }
            order.TotalAmount = totalAmount;
            order.Payment.AmountPaid = totalAmount;

            int result=await repo.AddAsync(order);
            await transaction.CommitAsync();
            return result;

        }
      


        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            var orders =await repo.GetAllAsync();
            return orders.Adapt<IEnumerable<OrderResponse>>();
        }


        public async Task<OrderFullResponse> GetOrderDetailsById(int id)
        {
            var order = await repo.GetOrderDetailsById(id);
            if(order is null)
            {
                return null;
            }


            return order.Adapt<OrderFullResponse>();
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByUserIdAsync(string id)
        {
            var orders = await repo.GetOrdersByUserId(id);


            return orders.Adapt<IEnumerable<OrderResponse>>();
        }
    }
}
