using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace JobFinder.Application.Feature.CompanyFollows.Command
{
    public class UnfollowCompanyCommand : MediatR.IRequest<Result>
    {
        public Guid CompanyId { get; set; }
        public string UserId { get; set; }
    }
}
