using Domain.Roles;
using JobFinder.Application.Feature.DropDowns.JobCategories.Command.CreateJobCategoryCommand;
using JobFinder.Application.Feature.DropDowns.JobCategories.Command.DeleteJobCategoryCommand;
using JobFinder.Application.Feature.DropDowns.JobCategories.Command.UpdateJobCategoryCommand;
using JobFinder.Application.Feature.DropDowns.JobCategories.Queries.GetJobCategoriesQuery;
using JobFinder.Application.Feature.DropDowns.JobCategories.Queries.GetJobCategoryByIdQuery;
using JobFinder.Contracts.Dtos.DropDown;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class JobCategoriesController : ApiController
    {
        [HttpGet("GetJobCategories")]
        public async Task<ActionResult<List<JobCategoryDto>>> GetJobCategories()
        {
            var query = new GetJobCategoriesQuery();
            var jobCategories = await Mediator.Send(query);
            if (jobCategories.IsSuccess)
            {
                return Ok(jobCategories);
            }
            else
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: jobCategories.Errors.ToString());
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetJobCategoryByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            if (result == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");

            return Ok(result);
        }


        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateJobCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("UpdateJobCategory/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateJobCategoryCommand command)
        {
            if (id != command.Id)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "BadRequest");

            var result = await Mediator.Send(command);

            if (!result)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");

            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("DeleteJobCategory/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteJobCategoryCommand { Id = id };
            var result = await Mediator.Send(command);

            if (!result)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");

            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }
}
