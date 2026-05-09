using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.CompanyBenefit
{
    public class CompanyBenefitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsVerified { get; set; }
    }
}
