using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Enums;
using MediatR;

namespace JobFinder.Application.Feature.Skill.Command
{
    public record UpdateSkillCommand(
        int Id,
        Guid ResumeId,
        string Name,
        ProficiencyLevelEnum ProficiencyLevel,
        bool? IsActive) : IRequest<bool>;

}
