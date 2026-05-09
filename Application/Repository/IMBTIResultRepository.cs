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


    public interface IMBTIResultRepository : IWriteRepository<MBTIResult> //, IReadRepository<MBTIResult>//: IRepository<MBTIResult>
    {
        Task<IEnumerable<MBTIResult>> GetAllAsyncMBTI();
        Task<MBTIResult> GetByIdAsyncMBTI(int id);
        Task AddAsyncMBTI(MBTIResult entity);
        void UpdateMBTI(MBTIResult entity);
        void DeleteMBTI(MBTIResult entity);
    }
}
