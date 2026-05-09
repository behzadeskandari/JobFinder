using FluentResults;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Contracts.Dtos.MbtiTest;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Services
{
    public class MBTICalculationService : IMBTICalculationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public MBTICalculationService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public Result<MBTIResultDTO> CalculateResult(Dictionary<Guid, string> answers, CancellationToken cancellationToken)
        {
            var categories = new Dictionary<string, int>
            {
                { "E", 0 }, { "I", 0 },
                { "S", 0 }, { "N", 0 },
                { "T", 0 }, { "F", 0 },
                { "J", 0 }, { "P", 0 }
            };

            var questions = _unitOfWork.MBTIQuestionRepository.GetAllAsync().Result;

            foreach (var answer in answers)
            {
                var question = questions.FirstOrDefault(q => q.Id == answer.Key);
                if (question != null)
                {
                    if (answer.Value == "yes")
                    {
                        categories[question.Category] += 1;
                    }
                    else
                    {
                        categories[InvertCategory(question.Category)] += 1;
                    }
                }
            }
            var userId = _currentUserService.UserId;
            var mbtiType = DetermineMBTIType(categories);
            var result = _unitOfWork.MBTIResultRepository.GetAllAsyncMBTI().Result.First(r => r.Name == mbtiType);
            var user =  _unitOfWork.UsersRepository.FindAsync(x => x.Id == userId).Result.FirstOrDefault();
            ICollection<MBTIQuestion> questionsList = questions as ICollection<MBTIQuestion>;
            var mbtiResult = new MBTIResultAnswer
            {
                Name = result.Name,
                Type = result.Type,
                Description = result.Description,
                Result = $"شما در دسته بندی  {mbtiType} قرار گرفته اید",
                UserId = userId,
            };
            _unitOfWork.MBTIResultAnswersRepository.AddAsync(mbtiResult);
            _unitOfWork.CommitAsync(cancellationToken);
            var resultDto = new MBTIResultDTO
            {
                Id = result.Id,
                Name = result.Name,
                Type = result.Type,
                Description = result.Description,
                Result = $"شما در دسته بندی  {mbtiType} قرار گرفته اید",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsActive = result.IsActive,
            };

            return Result.Ok(resultDto);
        }

        private string InvertCategory(string category)
        {
            return category switch
            {
                "E" => "I",
                "I" => "E",
                "S" => "N",
                "N" => "S",
                "T" => "F",
                "F" => "T",
                "J" => "P",
                "P" => "J",
                _ => category
            };
        }

        private string DetermineMBTIType(Dictionary<string, int> categories)
        {
            return $"{(categories["E"] >= categories["I"] ? "E" : "I")}" +
                   $"{(categories["S"] >= categories["N"] ? "S" : "N")}" +
                   $"{(categories["T"] >= categories["F"] ? "T" : "F")}" +
                   $"{(categories["J"] >= categories["P"] ? "J" : "P")}";
        }
    }
}
