using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.WriteRepository;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Repository
{
    public interface IJobPostsRepository : 
        IWriteRepository<JobFinder.Domain.Common.Entities.JobPost>
        //, IReadRepository<JobSeeker.Domain.Common.Entities.JobPost>
    {
        Task<IEnumerable<JobPost>> GetAllWithSkillsAsync();
        Task<JobPost> GetByIdWithSkillsAsync(Guid jobId);
    }
}
