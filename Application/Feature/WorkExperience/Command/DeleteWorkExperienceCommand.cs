using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.WorkExperience.Command
{
    public record DeleteWorkExperienceCommand(Guid Id) : IRequest<bool>;

}
