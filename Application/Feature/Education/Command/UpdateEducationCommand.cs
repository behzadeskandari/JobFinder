using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Education.Command
{
    public record UpdateEducationCommand(
        Guid Id,
        Guid ResumeId,
        string Degree,
        string Institution,
        string Field,
        DateTime StartDate,
        DateTime? EndDate,
        string Description,
        bool? IsActive) : IRequest<bool>;
}
