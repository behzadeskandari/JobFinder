using Domain.Roles;
using JobFinder.Application.Feature.WorkExperience.Command;
using JobFinder.Application.Feature.WorkExperience.Queries;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    [Authorize(Roles = Roles.User)]
    public class WorkExperiencesController : ApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<WorkExperience>>> GetAllWorkExperiences()
        {
            var query = new GetAllWorkExperiencesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkExperience>> GetWorkExperienceById(Guid id)
        {
            var query = new GetWorkExperienceByIdQuery(id);
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Status404NotFound");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateWorkExperience(CreateWorkExperienceCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetWorkExperienceById), new { id = result }, result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateWorkExperience(Guid id, UpdateWorkExperienceCommand command)
        {
            if (id != command.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "NoContent");
            }
            var result = await Mediator.Send(command);
            if (!result)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Status404NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteWorkExperience(Guid id)
        {
            var command = new DeleteWorkExperienceCommand(id);
            var result = await Mediator.Send(command);
            if (!result)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Status404NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

    }
}
