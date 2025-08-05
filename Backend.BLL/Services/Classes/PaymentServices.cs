using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using Backend.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Classes
{
    public class PaymentServices: GenericService<Payment, PaymentRequest, PaymentResponse>,IPaymentsServices
    {
        private readonly IPaymentRepository paymentRepository;
        public PaymentServices(IPaymentRepository repo):base(repo)
        {
            this.paymentRepository = repo;
        }

        public async Task<PaymentDetailesResponse> GetPaymentDetailesByIdAsync(int id)
        {
               var payment=await paymentRepository.GetPaymentDetailesByIdAsync(id);
            if (payment == null)
            {
                return null;
            }
            var response = payment.Adapt<PaymentDetailesResponse>();
            return response;
        }

        public async Task<PaymentDetailesResponse> GetPaymentDetailesByOrderIdAsync(int orderId)
        {
            var payment = await paymentRepository.GetPaymentDetailesByOrderIdAsync(orderId);
            if (payment == null)
            {
                return null;
            }
            var response = payment.Adapt<PaymentDetailesResponse>();
            return response;
        }

        public async Task<int> TogglePaymentStatusAsync(int id)
        {
            var payment = await paymentRepository.GetByIdAsync(id);
            if(payment == null)
            {
                return 0; // Payment not found
            }
            payment.Status = payment.Status == PaymentStatus.NotConfirmed ? PaymentStatus.Confirmed : PaymentStatus.NotConfirmed;

            var updatedPayment = await paymentRepository.UpdateAsync(payment);
            return updatedPayment;
        }
    }
}
