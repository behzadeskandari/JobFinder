using FluentResults;
using JobFinder.Contracts.Dtos.MbtiTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Common.Interfaces.Services
{
    public interface IMBTICalculationService
    {
        Result<MBTIResultDTO> CalculateResult(Dictionary<Guid, string> answers, CancellationToken cancellationToken);
    }
}
