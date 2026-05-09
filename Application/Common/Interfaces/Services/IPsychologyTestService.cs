using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.PsychologyTest;
using JobFinder.Contracts.Dtos.PsychologyTestQuestion;


namespace JobFinder.Application.Common.Interfaces.Services
{
    public interface IPsychologyTestService
    {
        Task<List<PsychologyTestQuestionDto>> GetTestQuestionsAsync(int testId);
        Task<Result> SubmitTestResponseAsync(PsychologyTestSubmissionDto submission);
    }
}
