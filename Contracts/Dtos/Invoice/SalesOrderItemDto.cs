using JobFinder.Contracts.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Invoice
{
    public class SalesOrderItemDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public ProductDto Product { get; set; }
    }
}
