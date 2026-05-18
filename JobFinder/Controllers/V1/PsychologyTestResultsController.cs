using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class PsychologyTestResultsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PsychologyTestResultsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPsychologyTestResults()
        {
            var psychologyTestResults = await _unitOfWork.psychologyTestResult.GetAllAsync();
            return Ok(psychologyTestResults);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPsychologyTestResult(int id)
        {
            var psychologyTestResult = await _unitOfWork.psychologyTestResult.GetByIdAsync(id);
            if (psychologyTestResult == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(psychologyTestResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePsychologyTestResult([FromBody] PsychologyTestResult psychologyTestResult)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            await _unitOfWork.psychologyTestResult.AddAsync(psychologyTestResult);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetPsychologyTestResult), new { id = psychologyTestResult.Id }, psychologyTestResult);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePsychologyTestResult(Guid id, [FromBody] PsychologyTestResult psychologyTestResult)
        {
            if (!ModelState.IsValid || id != psychologyTestResult.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingPsychologyTestResult = await _unitOfWork.psychologyTestResult.GetByIdAsync(id);
            if (existingPsychologyTestResult == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            _unitOfWork.psychologyTestResult.UpdateAsync(psychologyTestResult);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeletePsychologyTestResult(int id)
        {
            var psychologyTestResult = await _unitOfWork.psychologyTestResult.GetByIdAsync(id);
            if (psychologyTestResult == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.psychologyTestResult.DeleteAsync(psychologyTestResult);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }

}
