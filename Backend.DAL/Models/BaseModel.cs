using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Models
{
    public enum Status { 
    Active=1,
    InActive=2
    }

    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }=Status.InActive;
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
