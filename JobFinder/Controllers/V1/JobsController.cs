using AutoMapper;
using Domain.Roles;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Feature.DropDowns.JobCategories.Queries;
using JobFinder.Application.Feature.DropDowns.JobCategories.Queries.GetJobCategoriesQuery;
using JobFinder.Application.Feature.Job.Command;
using JobFinder.Application.Feature.Job.Query;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Contracts.Dtos.SavedJobs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{

    public class JobsController : ApiController 
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _account;

        public JobsController(IMapper mapper, IAccountService account)
        {
            _mapper = mapper;
            _account = account;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetAllJobs()
        {
            var query = new GetAllJobsQuery();
            var result = await Mediator.Send(query);
            if (result == null)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "BadRequest");
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.All)]
        public async Task<ActionResult<JobDto>> GetJobById(Guid id)
        {
            var query = new GetJobByIdQuery(id);
            var result = await Mediator.Send(query);
            if (result == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            return Ok(result);
        }

        [HttpPost("CreateJob")]
        [Authorize(Roles = Roles.Admin)]
        //[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Staff)] // Only Admin and Staff can create
        public async Task<ActionResult<int>> CreateJob(CreateJobCommand command)
        {
            var result = await Mediator.Send(command);
            if (result == null)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: result + "BadRequest");
            return CreatedAtAction(nameof(GetJobById), new { id = result }, result);
        }

        [HttpPost("{id}")]

        [Authorize(Roles = Roles.Admin)]
        //[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Staff)] // Only Admin and Staff can update
        public async Task<IActionResult> UpdateJob(Guid id, UpdateJobCommand command)
        {
            if (id != command.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "BadRequest");
            }
            var result = await Mediator.Send(command);
            if (result == null)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: result + "BadRequest");
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("{id}")]
        //[Authorize(Roles = SD.Role_Admin)] // Only Admin can delete
        public async Task<IActionResult> DeleteJob(int id)
        {
            var command = new DeleteJobCommand(id);
            var result = await Mediator.Send(command);
            if (result == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }


        //[HttpPost("apply")]
        //public async Task<ActionResult<JobApplicationDto>> ApplyJob([FromBody] ApplyJobCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Reasons);
        //}


        [Authorize(Roles = Roles.Admin)]
        [HttpGet("recommended")]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetRecommended()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await Mediator.Send(new GetRecommendedJobsQuery { UserId = userId });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
        }

        [HttpGet("saved")]

        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetSaved()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await Mediator.Send(new GetSavedJobsQuery { UserId = userId });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
        }

        [HttpGet("applied")]

        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetApplied()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await Mediator.Send(new GetAppliedJobsQuery { UserId = userId });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> Search([FromQuery] SearchJobsQueryDto searchCriteria)
        {
            var result = await Mediator.Send(new SearchJobsQuery { SearchCriteria = searchCriteria, PageNumber = searchCriteria.PageNumber, PageSize = searchCriteria.PageSize });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
        }

        [HttpPost("save/{jobId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<SavedJobDto>> SaveJob(Guid jobId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await Mediator.Send(new SaveJobCommand { JobId = jobId, UserId = userId });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
        }

        [HttpPost("save/{jobId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UnsaveJob(Guid jobId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await Mediator.Send(new UnsaveJobCommand { JobId = jobId, UserId = userId });
            return result.IsSuccess ? NoContent() : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
        }


        [HttpGet("categories")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<JobCategoryDto>>> GetCategories()
        {
            var result = await Mediator.Send(new GetJobCategoriesQuery());
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
        }

        [HttpGet("category/{slug}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetJobsByCategory(string slug)
        {
            var result = await Mediator.Send(new GetJobsByCategoryQuery { Slug = slug });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
        }

        [HttpGet("similar/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetSimilarJobs(Guid id)
        {
            var result = await Mediator.Send(new GetSimilarJobsQuery { Id = id });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
        }

        [HttpPost("apply")]

        [Authorize(Roles = Roles.All)]
        public async Task<ActionResult<JobApplicationDto>> ApplyJob([FromBody] ApplyJobCommand command)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            command.UserId = userId;
            var result = await Mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
        }
    }
}
