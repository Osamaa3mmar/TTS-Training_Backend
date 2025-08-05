using Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.DTO.Request
{
    public class ChangeOrderStatusRequest
    {
        public OrderStatus Status { get; set; }
    }
}
