using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Models
{
    public class Product:BaseModel
    {
        [MaxLength(60)]
        [MinLength(5)]
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [MaxLength(500)]
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }=new List<OrderDetails>();
    }
}
