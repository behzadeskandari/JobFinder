using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.WorkExperience.Command
{
    public record UpdateWorkExperienceCommand(
        Guid Id,
        Guid ResumeId,
        string JobTitle,
        string CompanyName,
        bool IsCurrentJob,
        string Description,
        bool? IsActive) : IRequest<bool>;
}
