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
    public class DeleteMBTIQuestionHandler : IRequestHandler<DeleteMBTIQuestionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMBTIQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteMBTIQuestionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.MBTIQuestionRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                throw new NotFoundException("سوال MBTI پیدا نشد");

            _unitOfWork.MBTIQuestionRepository.DeleteMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
