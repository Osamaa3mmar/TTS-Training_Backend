using Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.DTO.Request
{
    public class PaymentRequest
    {
        public string PaymentMethod { get; set; } = "cash";
        public PaymentStatus Status { get; set; } = PaymentStatus.NotConfirmed;

    }
}
