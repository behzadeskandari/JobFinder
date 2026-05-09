using Domain.WriteRepository;
using Microsoft.EntityFrameworkCore;
using Persistance.DatabaseContext.WriteDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Persistance.Repositories.GenericRepository
{
    public class GenericWriteRepository<T> : GenericRepository<T>, IWriteRepository<T> where T : class
    {
        private readonly WriteDbContext _writeContext;
        public GenericWriteRepository(WriteDbContext writeContext) : base(writeContext)
        {
            _writeContext = writeContext;
        }
    }
}
