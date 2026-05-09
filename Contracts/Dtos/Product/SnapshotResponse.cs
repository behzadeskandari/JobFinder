using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Product
{
    public class SnapshotResponse
    {
        public List<ProductInventorySnapshotDto> ProductInventorySnapshots { get; set; }
        public List<DateTime> Timeline { get; set; }
    }
}
