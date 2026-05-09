using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.CompanyBenefit;
using JobFinder.Contracts.Enums;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Command.CreateCompanyCommand
{
    public class CreateCompanyCommand : IRequest<Company>
    {
        public string Name { get; set; } = string.Empty;
        public CompanySize Size { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public DateTime FoundedDate { get; set; }
        // Relations
            
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public bool IsVerified { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        //public ICollection<JobSeeker.Domain.Common.Entities.Job> Jobs { get; set; } = new List<JobSeeker.Domain.Common.Entities.Job>();
        //public ICollection<JobSeeker.Domain.Common.Entities.Advertisement> Advertisements { get; set; } = new List<JobSeeker.Domain.Common.Entities.Advertisement>();
        public ICollection<CompanyBenefitDto> Benefits { get; set; } = new List<CompanyBenefitDto>();

        public int IndustryId { get; set; }
        public int CityId { get; set; }
        public decimal Rating { get; set; }
        public string LogoUrl { get; set; } = string.Empty;
    }
}
