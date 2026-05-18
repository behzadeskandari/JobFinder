using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class PsychologyTestResponsesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PsychologyTestResponsesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPsychologyTestResponses()
        {
            var psychologyTestResponses = await _unitOfWork.psychologyTestResponse.GetAllAsync();
            return Ok(psychologyTestResponses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPsychologyTestResponse(int id)
        {
            var psychologyTestResponse = await _unitOfWork.psychologyTestResponse.GetByIdAsync(id);
            if (psychologyTestResponse == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(psychologyTestResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePsychologyTestResponse([FromBody] PsychologyTestResponse psychologyTestResponse)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            await _unitOfWork.psychologyTestResponse.AddAsync(psychologyTestResponse);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetPsychologyTestResponse), new { id = psychologyTestResponse.Id }, psychologyTestResponse);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePsychologyTestResponse(Guid id, [FromBody] PsychologyTestResponse psychologyTestResponse)
        {
            if (!ModelState.IsValid || id != psychologyTestResponse.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingPsychologyTestResponse = await _unitOfWork.psychologyTestResponse.GetByIdAsync(id);
            if (existingPsychologyTestResponse == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.psychologyTestResponse.UpdateAsync(psychologyTestResponse);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeletePsychologyTestResponse(int id)
        {
            var psychologyTestResponse = await _unitOfWork.psychologyTestResponse.GetByIdAsync(id);
            if (psychologyTestResponse == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.psychologyTestResponse.DeleteAsync(psychologyTestResponse);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }

}
