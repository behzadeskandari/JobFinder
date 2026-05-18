using JobFinder.Application.Feature.FaqQuestion.Command;
using JobFinder.Application.Feature.FaqQuestion.Query;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class FaqQuestionsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<FaqQuestion>>> GetAllFaqQuestions()
        {
            var query = new GetAllFaqQuestionsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FaqQuestion>> GetFaqQuestionById(int id)
        {
            var query = new GetFaqQuestionByIdQuery(id);
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateFaqQuestion(CreateFaqQuestionCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetFaqQuestionById), new { id = result }, result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateFaqQuestion(int id, UpdateFaqQuestionCommand command)
        {
            if (id != command.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "BadRequest");
            }
            var result = await Mediator.Send(command);
            if (!result)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteFaqQuestion(int id)
        {
            var command = new DeleteFaqQuestionCommand(id);
            var result = await Mediator.Send(command);
            if (!result)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }

}
