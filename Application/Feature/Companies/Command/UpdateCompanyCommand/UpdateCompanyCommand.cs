using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.CompanyBenefit;
using JobFinder.Contracts.Enums;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Command.UpdateCompanyCommand
{
    public class UpdateCompanyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public int EmployeeCount { get; set; }
        public DateTime FoundedDate { get; set; }
        public CompanySize CompanySize { get; set; }
        public bool IsVerified { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public List<CompanyBenefitDto> Benefits { get; set; } = new List<CompanyBenefitDto>();
        public bool? IsActive { get; internal set; }
    }
}
