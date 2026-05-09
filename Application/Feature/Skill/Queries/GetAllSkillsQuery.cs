using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Skill.Queries
{
    public record GetAllSkillsQuery : IRequest<List<JobFinder.Domain.Common.Entities.Skill>>;

}
