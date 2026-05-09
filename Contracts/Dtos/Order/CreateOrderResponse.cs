using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Order
{

    public class CreateOrderResponse
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
