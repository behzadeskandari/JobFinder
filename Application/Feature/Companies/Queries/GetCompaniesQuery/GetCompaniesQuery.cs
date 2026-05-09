using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.Company;
using JobFinder.Domain.Common.Models;

namespace JobFinder.Application.Feature.Companies.Queries.GetCompaniesQuery
{
    public class GetCompaniesQuery : MediatR.IRequest<Result<PagedResult<CompanyDto>>>
    {
        public SearchCompaniesQueryDto SearchCriteria { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get;  set; }
    }
}
