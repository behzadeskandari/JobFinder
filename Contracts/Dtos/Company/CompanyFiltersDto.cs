using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Enums;

namespace JobFinder.Contracts.Dtos.Company
{
    public class CompanyFiltersDto
    {
        public IEnumerable<FilterOption> Industries { get; set; }
        public IEnumerable<FilterOption> Cities { get; set; }
        public IEnumerable<CompanySize> Sizes { get; set; }
        public IEnumerable<CompanyBenefit.CompanyBenefitDto> Benefits { get; set; }
    }
}
