using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.MBTI.Command;
using JobFinder.Contracts.Dtos.MbtiTest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.MBTI.Handlers
{
    public class UpdateMBTIResultHandler : IRequestHandler<UpdateMBTIResultCommand, Result<MBTIResultDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMBTIResultHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIResultDTO>> Handle(UpdateMBTIResultCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.MBTIResultRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                throw new NotFoundException("نتیجه MBTI یافت نشد");

            entity.Name = request.MBTIResult.Name;
            entity.Type = request.MBTIResult.Type;
            entity.Description = request.MBTIResult.Description;
            entity.Result = request.MBTIResult.Result;

            _unitOfWork.MBTIResultRepository.UpdateMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok(request.MBTIResult);
        }
    }
}
