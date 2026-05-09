using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Enums;
using MediatR;

namespace JobFinder.Application.Feature.Skill.Command
{
    public record CreateSkillCommand(
       int ResumeId,
       string Name,
       ProficiencyLevelEnum ProficiencyLevel,int CandidateId) : IRequest<int>;

}
