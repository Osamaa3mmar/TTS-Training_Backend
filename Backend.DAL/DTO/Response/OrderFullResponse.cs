using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.DTO.Response
{
    public class OrderFullResponse:OrderResponse
    {



        public ICollection<OrderDetailsResponse> OrderDetails { get; set; }
    }
}
