using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class PersonalityTestResponsesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonalityTestResponsesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonalityTestResponses()
        {
            var personalityTestResponses = await _unitOfWork.personalityTestResponse.GetAllAsync();
            return Ok(personalityTestResponses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonalityTestResponse(int id)
        {
            var personalityTestResponse = await _unitOfWork.personalityTestResponse.GetByIdAsync(id);
            if (personalityTestResponse == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(personalityTestResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonalityTestResponse([FromBody] PersonalityTestResponse personalityTestResponse)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            await _unitOfWork.personalityTestResponse.AddAsync(personalityTestResponse);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetPersonalityTestResponse), new { id = personalityTestResponse.Id }, personalityTestResponse);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePersonalityTestResponse(Guid id, [FromBody] PersonalityTestResponse personalityTestResponse)
        {
            if (!ModelState.IsValid || id != personalityTestResponse.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingPersonalityTestResponse = await _unitOfWork.personalityTestResponse.GetByIdAsync(id);
            if (existingPersonalityTestResponse == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.personalityTestResponse.UpdateAsync(personalityTestResponse);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeletePersonalityTestResponse(int id)
        {
            var personalityTestResponse = await _unitOfWork.personalityTestResponse.GetByIdAsync(id);
            if (personalityTestResponse == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.personalityTestResponse.DeleteAsync(personalityTestResponse);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }

}
