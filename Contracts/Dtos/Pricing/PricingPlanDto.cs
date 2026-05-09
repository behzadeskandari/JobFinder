using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Pricing
{
    public class PricingPlanDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int Duration { get; set; }
        public string DurationUnit { get; set; }
        public int JobCount { get; set; }
        public int? DiscountPercentage { get; set; }
        public List<PricingFeatureDto> Features { get; set; } = new List<PricingFeatureDto>();
        public string ButtonText { get; set; }
        public bool? IsPopular { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int PricingCategoryId { get; set; }
        public string PricingCategoryName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}

