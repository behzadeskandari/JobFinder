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
    public class UpdateMBTIQuestionHandler : IRequestHandler<UpdateMBTIQuestionCommand, Result<MBTIQuestionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMBTIQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIQuestionDTO>> Handle(UpdateMBTIQuestionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.MBTIQuestionRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                throw new NotFoundException("سوال MBTI پیدا نشد");

            entity.QuestionText = request.MBTIQuestion.QuestionText;
            entity.Category = request.MBTIQuestion.Category;
            entity.IsActive = request.MBTIQuestion.IsActive;

            _unitOfWork.MBTIQuestionRepository.UpdateMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok(request.MBTIQuestion);
        }
    }
}
