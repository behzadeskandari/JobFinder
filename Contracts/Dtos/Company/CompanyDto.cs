using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.CompanyBenefit;

namespace JobFinder.Contracts.Dtos.Company
{
    public class CompanyDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IndustryName { get; set; }
        public string CityName { get; set; }
        public string Size { get; set; }
        public List<CompanyBenefitDto> Benefits { get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }

        public bool? IsActive { get;  set; }
        public bool IsVerified { get;  set; }

    }
}
