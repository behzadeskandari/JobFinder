using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class PersonalityTestResultsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonalityTestResultsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonalityTestResults()
        {
            var personalityTestResults = await _unitOfWork.personalityTestResult.GetAllAsync();
            return Ok(personalityTestResults);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonalityTestResult(int id)
        {
            var personalityTestResult = await _unitOfWork.personalityTestResult.GetByIdAsync(id);
            if (personalityTestResult == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(personalityTestResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonalityTestResult([FromBody] PersonalityTestResult personalityTestResult)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            await _unitOfWork.personalityTestResult.AddAsync(personalityTestResult);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetPersonalityTestResult), new { id = personalityTestResult.Id }, personalityTestResult);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePersonalityTestResult(Guid id, [FromBody] PersonalityTestResult personalityTestResult)
        {
            if (!ModelState.IsValid || id != personalityTestResult.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingPersonalityTestResult = await _unitOfWork.personalityTestResult.GetByIdAsync(id);
            if (existingPersonalityTestResult == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.personalityTestResult.UpdateAsync(personalityTestResult);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeletePersonalityTestResult(int id)
        {
            var personalityTestResult = await _unitOfWork.personalityTestResult.GetByIdAsync(id);
            if (personalityTestResult == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.personalityTestResult.DeleteAsync(personalityTestResult);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }


        [HttpPost("GetPeronalityTestResultByUserId")]
        public async Task<IActionResult> GetPeronalityTestResultByUserId()
        {
            var _username = userName;
            var _userId = userId;

            var personalityTestResult = await _unitOfWork.personalityTestResult.getPersonalityTestResultByUserId(userId);
            if (personalityTestResult == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(personalityTestResult);
        }

        [HttpPost("GetPeronalityTestResultByUserName")]
        public async Task<IActionResult> GetPeronalityTestResultByUserName()
        {
            var _username = userName;
            var _userId = userId;

            var personalityTestResult = await _unitOfWork.personalityTestResult.getPersonalityTestResultByUserName(userName);
            if (personalityTestResult == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(personalityTestResult);
        }


    }

}
