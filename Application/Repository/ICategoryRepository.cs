using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Category;
using JobFinder.Contracts.Dtos.Advertisement;
using Domain.WriteRepository;

namespace JobFinder.Application.Repository
{

    public interface ICategoryRepository : IWriteRepository<Category>//, IReadRepository<Category>// IRepository<Category>
    {
        public Task AddAsync(Category category, CancellationToken cancellationToken);

        public Task<Category> GetByIdAsync(int id);
        public Task<Category> GetByIdAsyncWithAdvertisements(int id, CancellationToken cancellationToken);

        public Task<List<CategoryDto>> GetAllAsyncCategory(CancellationToken cancellationToken);

        public Task<Category> UpdateAsync(Category category);

        public Task DeleteAsync(int id, CancellationToken cancellationToken);
        public Task<bool> ExistsAsync(int id);

        public Task<bool> ExistsAsync(string name);

        public Task<bool> ExistsAsync(string name, int id);
        public Task<bool> ExistsAsync(string name, string description);
        public Task<bool> ExistsAsync(string name, string description, int id);
        


    }
}
