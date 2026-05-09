using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.CompanyFollows;

namespace JobFinder.Application.Feature.CompanyFollows.Queries
{
    public class GetCompanyFollowByIdQuery : MediatR.IRequest<Result<CompanyFollowDto>>
    {
        public Guid Id { get; set; }
    }
}
