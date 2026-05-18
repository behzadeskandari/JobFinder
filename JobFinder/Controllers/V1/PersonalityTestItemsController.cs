using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Contracts.Dtos.PersonalityTestItem;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Controllers.V1
{
    public class PersonalityTestItemsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonalityTestItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonalityTestItems()
        {
            var personalityTestItems = await _unitOfWork.personalityTestItem.GetAllAsync();
            return Ok(personalityTestItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonalityTestItem(int id)
        {
            var personalityTestItem = await _unitOfWork.personalityTestItem.GetByIdAsync(id);
            if (personalityTestItem == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(personalityTestItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonalityTestItem([FromBody] PersonalityTestItem personalityTestItem)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            await _unitOfWork.personalityTestItem.AddAsync(personalityTestItem);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetPersonalityTestItem), new { id = personalityTestItem.Id }, personalityTestItem);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePersonalityTestItem(Guid id, [FromBody] PersonalityTestItem personalityTestItem)
        {
            if (!ModelState.IsValid || id != personalityTestItem.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingPersonalityTestItem = await _unitOfWork.personalityTestItem.GetByIdAsync(id);
            if (existingPersonalityTestItem == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.personalityTestItem.UpdateAsync(personalityTestItem);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeletePersonalityTestItem(int id)
        {
            var personalityTestItem = await _unitOfWork.personalityTestItem.GetByIdAsync(id);
            if (personalityTestItem == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.personalityTestItem.DeleteAsync(personalityTestItem);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitTest([FromBody] PersonalityTestSubmissionDto dto)
        {
            var user = await _unitOfWork.UsersRepository.FindAsync(x => x.Id == dto.UserId);
            if (user == null)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid User");

            var responses = new List<PersonalityTestResponse>();
            var traitScores = new Dictionary<string, List<int>>();

            var submissionDate = DateTime.Now;

            foreach (var answer in dto.Answers)
            {
                var item = await _unitOfWork.personalityTestItem.GetQueryable()
                    .Include(x => x.PersonalityTrait)
                    .FirstOrDefaultAsync(x => x.Id == answer.PersonalityTestItemId);

                if (item == null)
                    continue;

                var traitName = item.PersonalityTrait.Name;
                var score = item.ScoringDirection == "Negative" ? (6 - answer.Response) : answer.Response;

                if (!traitScores.ContainsKey(traitName))
                    traitScores[traitName] = new List<int>();

                traitScores[traitName].Add(score);

                responses.Add(new PersonalityTestResponse
                {
                    UserId = dto.UserId,
                    PersonalityTestItemId = item.Id,
                    Response = answer.Response,
                    SubmissionDate = submissionDate,
                    DateCreated = submissionDate,
                    IsActive = true
                });
            }

            // Add responses to DB
            await _unitOfWork.personalityTestResponse.AddRangeAsync(responses);

            var result = new PersonalityTestResult
            {
                UserId = dto.UserId,
                SubmissionDate = submissionDate,
                DateCreated = submissionDate,
                IsActive = true,
                OpennessScore = GetAverage(traitScores, "Openness"),
                ConscientiousnessScore = GetAverage(traitScores, "Conscientiousness"),
                ExtraversionScore = GetAverage(traitScores, "Extraversion"),
                AgreeablenessScore = GetAverage(traitScores, "Agreeableness"),
                NeuroticismScore = GetAverage(traitScores, "Neuroticism"),
            };

            result.Interpretation = new List<PsychologyTestInterpretation>
{
                new PsychologyTestInterpretation
                {
                    Interpretation = GenerateInterpretation(result),
                    MaxScore =  GetMax(traitScores, "Openness").Value +
                                 GetMax(traitScores, "Conscientiousness").Value +
                                 GetMax(traitScores, "Extraversion").Value +
                                 GetMax(traitScores, "Agreeableness").Value +
                                 GetMax(traitScores, "Neuroticism").Value,
                    MinScore = GetMin(traitScores, "Openness").Value +
                                 GetMin(traitScores, "Conscientiousness").Value +
                                 GetMin(traitScores, "Extraversion").Value +
                                 GetMin(traitScores, "Agreeableness").Value +
                                 GetMin(traitScores, "Neuroticism").Value,
                }
            };

            await _unitOfWork.personalityTestResult.AddAsync(result);
            await _unitOfWork.CommitAsync();

            return Ok(result);
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var items = await _unitOfWork.personalityTestItem.GetQueryable()
                .Where(q => q.IsActive == true)
                .Select(q => new PersonalityTestItemDto
                {
                    Id = q.Id,
                    ItemText = q.ItemText,
                    ScoringDirection = q.ScoringDirection
                    //TraitType = q.TraitType,
                })
                .ToListAsync();

            return Ok(items);
        }
        private decimal? GetMax(Dictionary<string, List<int>> scores, string trait)
        {
            if (!scores.ContainsKey(trait) || scores[trait].Count == 0)
                return null;

            return Math.Round((decimal)scores[trait].Max(), 2);
        }
        private decimal? GetMin(Dictionary<string, List<int>> scores, string trait)
        {
            if (!scores.ContainsKey(trait) || scores[trait].Count == 0)
                return null;

            return Math.Round((decimal)scores[trait].Min(), 2);
        }


        private decimal? GetAverage(Dictionary<string, List<int>> scores, string trait)
        {
            if (!scores.ContainsKey(trait) || scores[trait].Count == 0)
                return null;

            return Math.Round((decimal)scores[trait].Average(), 2);
        }

        private string GenerateInterpretation(PersonalityTestResult result)
        {
            var lines = new List<string>();

            if (result.ExtraversionScore >= 4) lines.Add("You are highly extroverted.");
            if (result.AgreeablenessScore <= 2) lines.Add("You may struggle with empathy.");
            if (result.OpennessScore >= 4) lines.Add("You are very open to new experiences.");
            if (result.ConscientiousnessScore >= 4) lines.Add("You are very organized and reliable.");
            if (result.NeuroticismScore >= 4) lines.Add("You tend to be emotionally reactive.");

            return string.Join(" ", lines);
        }
    }

}
