using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.MBTI.Command;
using JobFinder.Contracts.Dtos.MbtiTest;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.MBTI.Handlers
{
    public class CreateMBTIResultHandler : IRequestHandler<CreateMBTIResultCommand, Result<MBTIResultDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMBTIResultHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIResultDTO>> Handle(CreateMBTIResultCommand request, CancellationToken cancellationToken)
        {
            var entity = new MBTIResult
            {
                Name = request.MBTIResult.Name,
                Type = request.MBTIResult.Type,
                Description = request.MBTIResult.Description,
                Result = request.MBTIResult.Result
            };

            await _unitOfWork.MBTIResultRepository.AddAsyncMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            request.MBTIResult.Id = entity.Id;
            return Result.Ok(request.MBTIResult);
        }
    }
}
