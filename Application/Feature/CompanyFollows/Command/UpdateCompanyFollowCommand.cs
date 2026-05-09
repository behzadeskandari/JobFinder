using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace JobFinder.Application.Feature.CompanyFollows.Command
{
    public class UpdateCompanyFollowCommand : MediatR.IRequest<Result>
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string UserId { get; set; }
        public bool? IsActive { get; set; }
    }
}
