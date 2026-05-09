using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Job;

namespace JobFinder.Application.Services.interfaces
{
    public interface IAdminAnalyticsService
    {
        Task<PlatformOverview> GetPlatformOverview();
        Task<IEnumerable<UserRegistrationTrend>> GetUserRegistrationTrends(DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<PlatformUsageMetrics>> GetPlatformUsageMetrics(DateTime? startDate = null, DateTime? endDate = null);
    }
}
