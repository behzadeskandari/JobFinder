using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.WriteRepository;

namespace Domain.IRepository
{
    public interface IRepository<T> :
       //IReadRepository<T>,
       IWriteRepository<T> where T : class
    {
    }
}
