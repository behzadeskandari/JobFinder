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
    public class GetMBTIQuestionsByIdHandler : IRequestHandler<GetMBTIQuestionsByIdQuery, Result<MBTIQuestionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMBTIQuestionsByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIQuestionDTO>> Handle(GetMBTIQuestionsByIdQuery request, CancellationToken cancellationToken)
        {
            // Fetch the MBTIQuestion by ID from the repository
            var entity = await _unitOfWork.MBTIQuestionRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                throw new NotFoundException("سوال MBTI پیدا نشد");

            // Map the entity to a DTO
            var dto = new MBTIQuestionDTO
            {
                Id = entity.Id,
                QuestionText = entity.QuestionText,
                Category = entity.Category,
                IsActive = entity.IsActive
            };

            // Return the DTO wrapped in a FluentResults object
            return Result.Ok(dto);
        }
    }
}
