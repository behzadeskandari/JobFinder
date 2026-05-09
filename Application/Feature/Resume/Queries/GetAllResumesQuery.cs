using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Resume.Queries
{
    public record GetAllResumesQuery : IRequest<List<JobFinder.Domain.Common.Entities.Resume>>;
}
