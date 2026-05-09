using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Skill.Command
{

    public record DeleteSkillCommand(int Id) : IRequest<bool>;
}
