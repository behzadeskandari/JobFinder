using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Menu;
using Persistance.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Persistance.DatabaseContext.WriteDbContext;

namespace JobFinder.Persistance.Repositories
{
    public class MenuItemRepository : IMenuRepository
    {
        private readonly WriteDbContext _context;

        public MenuItemRepository(WriteDbContext context)
        {
            _context = context;
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _context.MenuItems
                .Include(m => m.Children)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems
                .Include(m => m.Children)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetRootMenuItemsAsync()
        {
            return await _context.MenuItems
                .Where(m => !m.ParentId.HasValue)
                .Include(m => m.Children)
                .ToListAsync();
        }

        public async Task AddAsync(MenuItem entity)
        {
            await _context.MenuItems.AddAsync(entity);
        }

        public void Update(MenuItem entity)
        {
            _context.MenuItems.Update(entity);
        }

        public void Delete(MenuItem entity)
        {
            _context.MenuItems.Remove(entity);
        }

        public Task<MenuItem> FindAsync(int requestId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MenuItemDto>> GetMenuWithChildren(CancellationToken cancellationToken)
        {
            var record = _context.MenuItems
                .Select(m => new MenuItemDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Url = m.Url,
                    ParentId = m.ParentId,
                    IsActive = m.IsActive
                }).ToListAsync(cancellationToken);

            return record;
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
