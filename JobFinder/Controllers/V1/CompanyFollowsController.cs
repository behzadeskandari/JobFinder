using JobFinder.Application.Feature.CompanyFollows.Command;
using JobFinder.Application.Feature.CompanyFollows.Queries;
using JobFinder.Contracts.Dtos.CompanyFollows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class CompanyFollowsController : ApiController
    {


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyFollowDto>>> GetAll()
        {
            var result = await Mediator.Send(new GetAllCompanyFollowsQuery());
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
            //BadRequest(result.Reasons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyFollowDto>> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetCompanyFollowByIdQuery { Id = id });
            return result.IsSuccess ? Ok(result.Value) :
                Problem(statusCode: StatusCodes.Status404NotFound, detail: "Status404NotFound");
            //NotFound(result.Reasons);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyFollowDto>> Create([FromBody] CreateCompanyFollowCommand command)
        {
            var result = await Mediator.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateCompanyFollowCommand command)
        {
            if (id != command.Id) return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
            var result = await Mediator.Send(command);
            return result.IsSuccess ? NoContent() : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteCompanyFollowCommand { Id = id });
            return result.IsSuccess ? Problem(statusCode: StatusCodes.Status204NoContent, detail: "Status204NoContent") : Problem(statusCode: StatusCodes.Status404NotFound, detail: "Status404NotFound");
        }
    }

}
