using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.MBTI.Queries;
using JobFinder.Contracts.Dtos.MbtiTest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.MBTI.Handlers
{
    public class GetAllMBTIResultsHandler : IRequestHandler<GetAllMBTIResultsQuery, Result<List<MBTIResultDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMBTIResultsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<MBTIResultDTO>>> Handle(GetAllMBTIResultsQuery request, CancellationToken cancellationToken)
        {
            // Fetch all MBTI results from the repository
            var results = await _unitOfWork.MBTIResultRepository.GetAllAsyncMBTI();

            // Map the results to DTOs
            var dtos = results.Select(r => new MBTIResultDTO
            {
                Id = r.Id,
                Name = r.Name,
                Type = r.Type,
                Description = r.Description,
                Result = r.Result
            }).ToList();

            // Return the DTOs wrapped in a FluentResults object
            return Result.Ok(dtos);
        }
    }
}
