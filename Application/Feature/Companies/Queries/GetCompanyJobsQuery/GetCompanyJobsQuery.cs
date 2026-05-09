using FluentResults;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Domain.Common.Models;
using MediaBrowser.Model.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Companies.Queries.GetCompanyJobsQuery
{
    public class GetCompanyJobsQuery : MediatR.IRequest<Result<PagedResult<JobGetDto>>>
    {
        public Guid CompanyId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
