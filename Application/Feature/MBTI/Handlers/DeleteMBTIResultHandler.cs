using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.MBTI.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.MBTI.Handlers
{
    public class DeleteMBTIResultHandler : IRequestHandler<DeleteMBTIResultCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMBTIResultHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteMBTIResultCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.MBTIResultRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                throw new NotFoundException("نتیجه MBTI یافت نشد");

            _unitOfWork.MBTIResultRepository.DeleteMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
