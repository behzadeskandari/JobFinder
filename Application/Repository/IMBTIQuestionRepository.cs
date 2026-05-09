using Domain.WriteRepository;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Repository
{

    public interface IMBTIQuestionRepository : IWriteRepository<MBTIQuestion> //, IReadRepository<MBTIQuestion>//IRepository<MBTIQuestion>
    {
        Task<IEnumerable<MBTIQuestion>> GetAllAsync();
        Task<MBTIQuestion> GetByIdAsyncMBTI(int id);
        Task AddAsyncMBTI(MBTIQuestion entity);
        void UpdateMBTI(MBTIQuestion entity);
        void DeleteMBTI(MBTIQuestion entity);
    }
}
