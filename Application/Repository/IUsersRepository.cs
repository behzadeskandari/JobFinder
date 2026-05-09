using Domain.WriteRepository;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Repository
{
    public interface IUsersRepository :
        //IReadRepository<User>,
        IWriteRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetRolesAsync(string userId);
    }
}
