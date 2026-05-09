using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.TermsSection.Command
{
    public record CreateTermsSectionCommand(string Title, string Content,int TermsOfServiceId) : IRequest<int>;
}
