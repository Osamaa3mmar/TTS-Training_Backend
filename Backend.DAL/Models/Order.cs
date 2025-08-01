using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Models
{
    public enum OrderStatus {
    Canceled = 0,
    Delivered = 1,
    Pending=2,
    OnRoad = 3,
    }

    public class Order:BaseModel
    {

        [Required]
        public DateTime OrderDate { get; set; }= DateTime.Now;
        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;


        [Column(TypeName ="decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }
        public Payment Payment { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }=new List<OrderDetails>();

    }
}
