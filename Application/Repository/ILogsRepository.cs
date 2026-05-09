using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;


namespace JobFinder.Application.Repository
{
    public interface ILogsRepository  //: IWriteRepository<Logs>
    {
        public Logs AddLogs(Logs logs);
        public Task<int> DeleteLogs(Logs logs);
        public Task DeleteLogsBatch(List<Logs> logs);
        Task<IEnumerable<Logs>> GetAllAsync();
    }
}
