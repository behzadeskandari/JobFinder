using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Pricing
{
    public class PricingCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
        public string Language { get; set; }
        public List<PricingPlanDto> Plans { get; set; } = new List<PricingPlanDto>();

        public bool IsActive { get; set; }
    }
}
