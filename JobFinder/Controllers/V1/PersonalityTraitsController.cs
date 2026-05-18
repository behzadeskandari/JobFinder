using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Contracts.Dtos.PersonalityTrait;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class PersonalityTraitsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonalityTraitsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonalityTraits()
        {
            var personalityTraits = await _unitOfWork.personalityTrait.GetAllAsync();
            return Ok(personalityTraits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonalityTrait(int id)
        {
            var personalityTrait = await _unitOfWork.personalityTrait.GetByIdAsync(id);
            if (personalityTrait == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(personalityTrait);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonalityTrait([FromBody] PersonalityTraitDto personalityTrait)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            PersonalityTrait personality = new PersonalityTrait()
            {
                IsActive = true,
                Name = personalityTrait.Name,
                Description = personalityTrait.Description,
                PersonalityTestItems = new List<PersonalityTestItem>(),

            };
            await _unitOfWork.personalityTrait.AddAsync(personality);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetPersonalityTrait), new { id = personality.Id }, personality);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePersonalityTrait(Guid id, [FromBody] PersonalityTrait personalityTrait)
        {
            if (!ModelState.IsValid || id != personalityTrait.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingPersonalityTrait = await _unitOfWork.personalityTrait.GetByIdAsync(id);
            if (existingPersonalityTrait == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.personalityTrait.UpdateAsync(personalityTrait);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeletePersonalityTrait(int id)
        {
            var personalityTrait = await _unitOfWork.personalityTrait.GetByIdAsync(id);
            if (personalityTrait == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.personalityTrait.DeleteAsync(personalityTrait);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }

}
