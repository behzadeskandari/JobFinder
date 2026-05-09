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
    public record UpdateJobCommand(Guid Id, string Title, JobLevel Level, Guid CompanyId, bool IsProirity, JobType JobType, string? JobDescription, string? JobRequirment, int? JobRequestsId, int? CityId, Guid? FeaturesId, int? TechnicalOptionsId, Guid? OrderId, int JobCategoryId, bool? IsActive) : IRequest<bool>;

}
