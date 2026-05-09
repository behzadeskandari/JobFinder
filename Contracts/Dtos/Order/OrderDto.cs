using JobFinder.Contracts.Dtos.Customer;
using JobFinder.Contracts.Dtos.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public CustomerDto Customer { get; set; }
        public List<SalesOrderItemDto> SalesOrderItems { get; set; }
        public bool IsPaid { get; set; }
    }
}
