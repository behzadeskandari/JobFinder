using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.WriteRepository;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Repository
{
    public interface IFeaturesRepository :
        IWriteRepository<JobFinder.Domain.Common.Entities.Feature>
//, IReadRepository<JobSeeker.Domain.Common.Entities.Feature>
    {
    }
}
