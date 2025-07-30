using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Models
{
    public class Category:BaseModel
    {
        [MaxLength(20)]
        [MinLength(5)]
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }

        ICollection<Product> Products { get; set; }=new List<Product>();
    }
}
