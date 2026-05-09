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
    public class CreateMBTIQuestionHandler : IRequestHandler<CreateMBTIQuestionCommand, Result<MBTIQuestionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMBTIQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIQuestionDTO>> Handle(CreateMBTIQuestionCommand request, CancellationToken cancellationToken)
        {
            var entity = new MBTIQuestion
            {
                QuestionText = request.MBTIQuestion.QuestionText,
                Category = request.MBTIQuestion.Category,
                IsActive = request.MBTIQuestion.IsActive
            };

            await _unitOfWork.MBTIQuestionRepository.AddAsyncMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            request.MBTIQuestion.Id = entity.Id;
            return Result.Ok(request.MBTIQuestion);
        }
    }
}
