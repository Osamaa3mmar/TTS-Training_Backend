using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Models
{
    public enum PaymentStatus
    {
        Confirmed=1,
        NotConfirmed=0,
    }
    public class Payment:BaseModel
    {
        [Required]
        [MaxLength(20)]
        public string PaymentMethod { get; set; } = "cash";
        [Required]
        public decimal AmountPaid { get; set; } = 100;
        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.NotConfirmed;

        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
