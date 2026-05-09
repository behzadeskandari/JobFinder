using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.CompanyFollows;

namespace JobFinder.Application.Feature.CompanyFollows.Queries
{
    public class GetAllCompanyFollowsQuery : MediatR.IRequest<Result<IEnumerable<CompanyFollowDto>>>
    {
        public string UserId { get; set; }
    }
}
