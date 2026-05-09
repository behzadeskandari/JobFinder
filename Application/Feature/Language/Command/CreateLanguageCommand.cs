using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Enums;
using MediatR;

namespace JobFinder.Application.Feature.Language.Command
{
    public record CreateLanguageCommand(Guid ResumeId, string Name, ProficiencyLevelEnum ProficiencyLevel) : IRequest<Guid>;

}
