using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.MBTI.Queries;
using JobFinder.Contracts.Dtos.MbtiTest;
using JobFinder.Contracts.Dtos.PsychologyTestQuestion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.MBTI.Handlers
{
    public class GetMBTIQuestionsHandler : IRequestHandler<GetMBTIQuestionsQuery, Result<List<MBTIQuestionDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMBTIQuestionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<MBTIQuestionDTO>>> Handle(GetMBTIQuestionsQuery request, CancellationToken cancellationToken)
        {


            var questions = (await _unitOfWork.MBTIQuestionRepository.GetAllAsync())
                           .Select(q => new MBTIQuestionDTO
                           {
                               Id = q.Id,
                               QuestionText = q.QuestionText,
                               Answers = new List<AnswerDtoMBTI>{
                                    new AnswerDtoMBTI
                                    {
                                        QuestionId = q.Id,
                                        Score = 1, //Yes 
                                    },
                                    new AnswerDtoMBTI
                                    {
                                        QuestionId = q.Id,
                                        Score = 2, //No
                                    }
                               }

                           })
                           .ToList();

            return Result.Ok(questions);
        }
    }
}
