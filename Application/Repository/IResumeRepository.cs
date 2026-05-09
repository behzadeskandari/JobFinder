using Domain.WriteRepository;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Repository
{
  
    public interface IResumeRepository : IWriteRepository<Resume>//, IReadRepository<Resume>//IRepository<Resume>
    {


        Task<Resume> GetResume(Guid id);
        Task<Resume> CreateResume(Resume resume);

        Task<Resume> UpdateResume(Guid id,Resume resume);
        Task<Resume> DeleteResume(Resume resume);
        Task<Resume> GetResumePdf(Guid id);
        Task<bool> ResumeExists(Guid id);
    }
}
