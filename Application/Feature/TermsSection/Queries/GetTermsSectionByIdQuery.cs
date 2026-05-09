using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.TermsSection.Queries
{
    public record GetTermsSectionByIdQuery(int Id) : IRequest<JobFinder.Domain.Common.Entities.TermsSection>;
}
