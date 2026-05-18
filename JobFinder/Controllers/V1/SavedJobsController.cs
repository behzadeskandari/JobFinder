using JobFinder.Application.Feature.SavedJobs.Commands;
using JobFinder.Application.Feature.SavedJobs.Query;
using JobFinder.Contracts.Dtos.SavedJobs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class SavedJobsController : ApiController
    {

    

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SavedJobDto>>> GetAll()
        {
            var result = await Mediator.Send(new GetAllSavedJobsQuery());
            return result.IsSuccess ? Ok(result.Value) :
                Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());// BadRequest(result.Reasons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SavedJobDto>> GetById(int id)
        {
            var result = await Mediator.Send(new GetSavedJobByIdQuery { Id = id });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status404NotFound, detail: result.Reasons.ToString());
        }

        [HttpPost]
        public async Task<ActionResult<SavedJobDto>> Create([FromBody] CreateSavedJobCommand command)
        {
            var result = await Mediator.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : BadRequest(result.Reasons);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateSavedJobCommand command)
        {
            if (id != command.Id) return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Miss Match Id");
            var result = await Mediator.Send(command);
            return result.IsSuccess ? Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent") :
                Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
            //BadRequest(result.Reasons);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteSavedJobCommand { Id = id });
            return result.IsSuccess ? NoContent() : Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
        }
    }

}
