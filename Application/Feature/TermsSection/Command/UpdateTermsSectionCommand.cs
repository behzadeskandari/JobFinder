using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.TermsSection.Command
{
    public record UpdateTermsSectionCommand(int Id, string Title, string Content, bool? IsActive) : IRequest<bool>;
}
