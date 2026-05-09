using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Shipment
{
    public class ShipmentDto
    {
        public Guid ProductId { get; set; }
        public int Adjustment { get; set; }
    }
}
