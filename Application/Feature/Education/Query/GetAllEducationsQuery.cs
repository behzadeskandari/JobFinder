using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Education.Query
{

    public record GetAllEducationsQuery : IRequest<List<JobFinder.Domain.Common.Entities.Education>>;
}
