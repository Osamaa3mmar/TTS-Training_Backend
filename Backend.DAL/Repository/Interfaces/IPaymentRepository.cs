using Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository.Interfaces
{
    public interface IPaymentRepository:IGenericRepository<Payment>
    {


        public Task<Payment> GetPaymentDetailesByOrderIdAsync(int orderId);
        public Task<Payment> GetPaymentDetailesByIdAsync(int id);
    }
}
