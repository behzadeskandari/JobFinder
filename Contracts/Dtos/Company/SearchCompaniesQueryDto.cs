using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.CompanyBenefit;
using JobFinder.Contracts.Enums;

namespace JobFinder.Contracts.Dtos.Company
{
    public class SearchCompaniesQueryDto
    {
        public string Name { get; set; }
        public int? IndustryId { get; set; }
        public int? CityId { get; set; }
        public CompanySize Size { get; set; }
        public CompanyBenefit.CompanyBenefitDto Benefits { get; set; }
        public decimal? MinRating { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
