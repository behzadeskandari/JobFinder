using JobFinder.Contracts.Dtos.Menu;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Repository
{
    public interface IMenuRepository
    {

        Task<MenuItem> GetByIdAsync(int id);
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<IEnumerable<MenuItem>> GetRootMenuItemsAsync();
        Task AddAsync(MenuItem entity);
        void Update(MenuItem entity);
        void Delete(MenuItem entity);
        Task<MenuItem> FindAsync(int requestId);
        Task<List<MenuItemDto>> GetMenuWithChildren(CancellationToken cancellationToken);
        void save();
    }
}
