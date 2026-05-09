using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.WorkExperience.Queries
{
    public record GetAllWorkExperiencesQuery : IRequest<List<JobFinder.Domain.Common.Entities.WorkExperience>>;

}
