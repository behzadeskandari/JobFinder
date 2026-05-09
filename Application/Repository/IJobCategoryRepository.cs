using Domain.WriteRepository;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Repository
{

    public interface IJobCategoryRepository : IWriteRepository<JobCategory>//, IReadRepository<JobCategory> /// IRepository<JobCategory>
    {
        Task<IEnumerable<JobCategory>> GetAllAsync();
        Task<JobCategory> GetByIdAsync(int id);

        Task<IEnumerable<JobCategoryDto>> GetJobCategories();

        Task<JobCategory> AddAsyncJobCategory(JobCategory jobCategory);
        Task JobCategoryUpdateAsync(JobCategory jobCategory);
        Task DeleteAsync(int id);
    }
}
