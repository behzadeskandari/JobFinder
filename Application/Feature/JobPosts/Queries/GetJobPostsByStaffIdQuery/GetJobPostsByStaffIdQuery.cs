using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.JobPosts.Queries.GetJobPostsByStaffIdQuery
{
    public record GetJobPostsByStaffIdQuery(string StaffId) : IRequest<Result<List<JobPost>>>;
}
