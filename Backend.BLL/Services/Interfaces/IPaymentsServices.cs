using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using Backend.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Interfaces
{
    public  interface IPaymentsServices:IGenericService<Payment,PaymentRequest,PaymentResponse>
    {


        public  Task<PaymentDetailesResponse> GetPaymentDetailesByOrderIdAsync(int orderId);


        public  Task<PaymentDetailesResponse> GetPaymentDetailesByIdAsync(int id);

        public Task<int> TogglePaymentStatusAsync(int id);

    }
}
