using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Product
{
    public class ProductInventorySnapshotDto
    {
        public List<int> QuantityOnHand { get; set; }
        public Guid ProductId { get; set; }
    }
}
