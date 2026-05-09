using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Common.Interfaces.Services
{
    public interface IPdfService
    {
        Task<byte[]> GenerateResumePdf(Resume resume);

        Task<byte[]> GeneratePdfAsync(TermsOfService terms);
    }

}
