using Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.DTO.Response
{
    public class PaymentResponse
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; } = "cash";
        public decimal AmountPaid { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.NotConfirmed;

        public int OrderId { get; set; }
    }
}
