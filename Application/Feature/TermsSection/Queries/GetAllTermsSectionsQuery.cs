using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.TermsSection.Queries
{
    public record GetAllTermsSectionsQuery : IRequest<List<JobFinder.Domain.Common.Entities.TermsSection>>;
}
