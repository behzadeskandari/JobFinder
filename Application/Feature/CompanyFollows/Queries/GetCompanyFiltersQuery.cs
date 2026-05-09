using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.Company;

namespace JobFinder.Application.Feature.CompanyFollows.Queries
{
    public class GetCompanyFiltersQuery : MediatR.IRequest<Result<CompanyFiltersDto>>
    {
    }
}
