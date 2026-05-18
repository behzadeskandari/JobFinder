using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Contracts.Dtos.PsychologyTest;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class PsychologyTestsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPsychologyTestService _service;
        public PsychologyTestsController(IUnitOfWork unitOfWork, IPsychologyTestService service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPsychologyTests()
        {
            var psychologyTests = await _unitOfWork.psychologyTest.GetAllAsync();
            return Ok(psychologyTests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPsychologyTest(int id)
        {
            var psychologyTest = await _unitOfWork.psychologyTest.GetByIdAsync(id);
            if (psychologyTest == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(psychologyTest);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePsychologyTest([FromBody] PsychologyTest psychologyTest)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            await _unitOfWork.psychologyTest.AddAsync(psychologyTest);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetPsychologyTest), new { id = psychologyTest.Id }, psychologyTest);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePsychologyTest(int id, [FromBody] PsychologyTest psychologyTest)
        {
            if (!ModelState.IsValid || id != psychologyTest.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingPsychologyTest = await _unitOfWork.psychologyTest.GetByIdAsync(id);
            if (existingPsychologyTest == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.psychologyTest.UpdateAsync(psychologyTest);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeletePsychologyTest(int id)
        {
            var psychologyTest = await _unitOfWork.psychologyTest.GetByIdAsync(id);
            if (psychologyTest == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.psychologyTest.DeleteAsync(psychologyTest);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("GetPsychologyTestBySpecificUser/{id}")]
        public async Task<IActionResult> GetPsychologyTestBySpecificUser(int id)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(id);
            if (user == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            var currentUserTest = _unitOfWork.psychologyTest.GetQueryable().Where(x => x.UserId.Equals(user.Id));

            if (currentUserTest == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(currentUserTest);
        }

        [HttpPost("GetPsychologyTestByCurrentUser/{psychologyTestId}")]
        public async Task<IActionResult> GetPsychologyTestByCurrentUser(int psychologyTestId)
        {
            var _userName = userName;
            var _userId = userId;
            if (_userId == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            var psychologyTest = await _unitOfWork.psychologyTest.GetByIdAsync(psychologyTestId);
            if (psychologyTest == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(psychologyTest);
        }


        [HttpGet("{testId}/questions")]
        public async Task<IActionResult> GetQuestions(int testId)
        {
            var questions = await _service.GetTestQuestionsAsync(testId);
            return Ok(questions);
        }

        [Authorize(Roles = "User")]
        [HttpPost("submit")]
        public async Task<IActionResult> Submit(PsychologyTestSubmissionDto dto)
        {
            var result = await _service.SubmitTestResponseAsync(dto);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else if (result.IsFailed)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
            }
            else
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
            }
        }
    }

}
