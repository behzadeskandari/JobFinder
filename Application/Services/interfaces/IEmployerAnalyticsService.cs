using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Candidate;

namespace JobFinder.Application.Services.interfaces
{
    public interface IEmployerAnalyticsService
    {
        Task<IEnumerable<EmployerJobPostPerformance>> GetEmployerJobPostPerformances(int employerId, DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<ApplicationSource>> GetApplicationSources(int employerId, DateTime? startDate = null, DateTime? endDate = null);
    }
}
