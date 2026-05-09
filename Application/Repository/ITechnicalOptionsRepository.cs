using Domain.WriteRepository;
using JobFinder.Contracts.Dtos.Advertisement;
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

    public interface ITechnicalOptionsRepository : IWriteRepository<TechnicalOption> //, IReadRepository<TechnicalOption>//IRepository<TechnicalOption>
    {
        Task AddAsyncTechnical(TechnicalOption option);
        void DeleteTechnical(TechnicalOption option);
        Task<TechnicalOption> GetByIdAsyncTechnical(int id);
        Task<IEnumerable<TechnicalOptionDto>> GetTechnicalOptionsTechnical();

    }
}
