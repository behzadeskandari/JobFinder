using Domain.WriteRepository;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Repository
{
    public interface IFaqQuestionsRepository : IWriteRepository<FaqQuestion>//, IReadRepository<FaqQuestion>
    {
    }
}
