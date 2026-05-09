using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.TermsSection.Command;
using MediatR;

namespace JobFinder.Application.Feature.TermsOfService.Command
{
    public record CreateTermsOfServiceCommand(string Version, string LastUpdated, bool? IsActive, List<CreateTermsSectionCommand> Sections) : IRequest<int>;

}
