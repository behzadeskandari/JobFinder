using JobFinder.Application.Feature.Skill.Command;
using JobFinder.Application.Feature.Skill.Queries;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class SkillsController : ApiController
    {

        public SkillsController(IMediator mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<Skill>>> GetAllSkills()
        {
            var query = new GetAllSkillsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkillById(int id)
        {
            var query = new GetSkillByIdQuery(id);
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateSkill(CreateSkillCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetSkillById), new { id = result }, result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, UpdateSkillCommand command)
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
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var command = new DeleteSkillCommand(id);
            var result = await Mediator.Send(command);
            if (!result)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }

}
