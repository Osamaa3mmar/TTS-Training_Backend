using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.DTO.Request
{
    public class OrderDetailsRequest
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
