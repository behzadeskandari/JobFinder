using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Common.Interfaces.Services
{

    public interface ITermsService
    {
        Task<TermsOfService> GetEmployerTermsAsync();
    }
}
