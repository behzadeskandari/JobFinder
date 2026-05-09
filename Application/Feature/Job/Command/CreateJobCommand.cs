using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Enums;
using MediatR;

namespace JobFinder.Application.Feature.Job.Command
{
    public record CreateJobCommand : IRequest<Guid> {


        public string Title { get; set; }
        public JobLevel Level { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsProirity { get; set; }
        public JobType JobType { get; set; }
        public string? JobDescription { get; set; }
        public string? JobRequirment { get; set; }
        public Guid? JobRequestsId { get; set; }
        public int? CityId { get; set; }
        public Guid? FeaturesId { get; set; }
        public int? TechnicalOptionsId { get; set; }
        public Guid? OrderId { get; set; }
        public int JobCategoryId { get; set; }

    }

}
