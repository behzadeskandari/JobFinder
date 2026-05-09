using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.WriteRepository;

namespace JobFinder.Application.Repository
{
    public interface IJobRequestsRepository : 
        IWriteRepository<JobFinder.Domain.Common.Entities.JobRequest>
        //, IReadRepository<JobSeeker.Domain.Common.Entities.JobRequest>
    {
    }
}
