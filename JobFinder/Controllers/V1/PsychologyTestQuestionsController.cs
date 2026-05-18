using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class PsychologyTestQuestionsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PsychologyTestQuestionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPsychologyTestQuestions()
        {
            var psychologyTestQuestions = await _unitOfWork.psychologyTestQuestion.GetAllAsync();
            return Ok(psychologyTestQuestions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPsychologyTestQuestion(int id)
        {
            var psychologyTestQuestion = await _unitOfWork.psychologyTestQuestion.GetByIdAsync(id);
            if (psychologyTestQuestion == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(psychologyTestQuestion);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePsychologyTestQuestion([FromBody] PsychologyTestQuestion psychologyTestQuestion)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            await _unitOfWork.psychologyTestQuestion.AddAsync(psychologyTestQuestion);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetPsychologyTestQuestion), new { id = psychologyTestQuestion.Id }, psychologyTestQuestion);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePsychologyTestQuestion(Guid id, [FromBody] PsychologyTestQuestion psychologyTestQuestion)
        {
            if (!ModelState.IsValid || id != psychologyTestQuestion.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingPsychologyTestQuestion = await _unitOfWork.psychologyTestQuestion.GetByIdAsync(id);
            if (existingPsychologyTestQuestion == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.psychologyTestQuestion.UpdateAsync(psychologyTestQuestion);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeletePsychologyTestQuestion(int id)
        {
            var psychologyTestQuestion = await _unitOfWork.psychologyTestQuestion.GetByIdAsync(id);
            if (psychologyTestQuestion == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.psychologyTestQuestion.DeleteAsync(psychologyTestQuestion);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }

}
