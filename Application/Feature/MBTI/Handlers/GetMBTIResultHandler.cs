using FluentResults;
using JobFinder.Application.Common.Exceptions;
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
    public class GetMBTIResultHandler : IRequestHandler<GetMBTIResultQuery, Result<MBTIResultDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMBTIResultHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIResultDTO>> Handle(GetMBTIResultQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.MBTIResultRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                throw new NotFoundException("نتیجه MBTI یافت نشد");

            var dto = new MBTIResultDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                Description = entity.Description,
                Result = entity.Result
            };

            return Result.Ok(dto);
        }
    }
}
