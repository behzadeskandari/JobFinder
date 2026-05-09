using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.WriteRepository;
using FluentResults;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Contracts.Dtos.Company;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Repository
{


    public interface ICompanyRepository : 
        //IReadRepository<Company>,
        IWriteRepository<Company>// IRepository<Company>
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<IEnumerable<Company>> SearchAsync(string searchTerm, string industry = null, string location = null);
        Task<Company> GetByIdAsync(int id);
        Task<Company> AddAsync(Company company);
        Task UpdateCompanyAsync(Company company);
        Task DeleteAsync(int id);
        Task<IEnumerable<string>> GetAllIndustriesAsync();
        Task<IEnumerable<string>> GetAllLocationsAsync();

        Task<CompanyDto> GetCompanyByUserIdAsync(string userId);
        Task<Result<CompanyDto>> UpdateCompanyProfileAsync(string userId, CompanyDto updateDto);
        Task<CompanyDto> GetDashboardStatsAsync(string userId);
    }
}
