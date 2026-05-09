using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Order
{
    public class OrderGetDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PricingPlanId { get; set; }
        public string PricingPlanName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
