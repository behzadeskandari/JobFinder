using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.WriteRepository;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Repository
{
    public interface ISkillsRepository : IWriteRepository<Skill>//, IReadRepository<Skill>
    {
        Task<IEnumerable<Skill>> GetByCandidatesIds(IEnumerable<Candidate> candidates);
    }
}
