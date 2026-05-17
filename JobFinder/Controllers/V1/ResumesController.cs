using AutoMapper;
using Domain.Roles;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Feature.Resume.Command;
using JobFinder.Application.Feature.Resume.Queries;
using JobFinder.Contracts.Dtos.Resume;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class ResumesController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ResumesController(IMapper mapper, IMediator mediator, ICurrentUserService currentUserService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        /// <summary>
        /// Get the current user's resume
        /// </summary>
        /// <returns>The current user's resume</returns>
        [HttpGet("my")]
        [ProducesResponseType(typeof(ResumeDto), 200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = Roles.AdminAndUser)]
        public async Task<IActionResult> GetMyResume()
        {
            var userId = _currentUserService.UserId;
            var query = new GetResumeByUserIdQuery { UserId = userId };
            var result = await Mediator.Send(query);

            if (result.IsFailed)
            {
                return NotFound(new { Errors = result.Errors });
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Get a resume by ID (Admin/Staff only)
        /// </summary>
        /// <param name="id">The ID of the resume to get</param>
        /// <returns>The requested resume</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = Roles.StaffAndAbove)]
        [ProducesResponseType(typeof(ResumeDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetResume(Guid id)
        {
            var query = new GetResumeByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            if (result.IsFailed)
            {
                return NotFound(new { Errors = result.Errors });
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Create a new resume
        /// </summary>
        /// <param name="request">The resume details</param>
        /// <returns>The created resume</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(400)]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> CreateResume([FromBody] CreateResumeRequestDto request)
        {
            var userId = _currentUserService.UserId;
            var workExperiences = _mapper.Map<List<WorkExperience>>(request.WorkExperiences);
            var educations = _mapper.Map<List<Education>>(request.Educations);
            var skills = _mapper.Map<List<Skill>>(request.Skills);
            var languages = _mapper.Map<List<Language>>(request.Languages);
            var resume = new Resume()
            {
                UserId = userId,
                Address = request.Address,
                Email = request.Email,
                Phone = request.Phone,
                ProfilePictureUrl = request.ProfilePictureUrl,
                Summary = request.Summary,
                WorkExperiences = workExperiences,
                Educations = educations,
                Skills = skills,
                Languages = languages
            };

            var command = new CreateResumeCommand
            {
                Resume = resume
            };

            var result = await Mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return CreatedAtAction(nameof(GetResume), new { id = result.Value }, new { id = result.Value });
        }

        /// <summary>
        /// Update an existing resume
        /// </summary>
        /// <param name="id">The ID of the resume to update</param>
        /// <param name="request">The updated resume details</param>
        /// <returns>No content if successful</returns>
        [HttpPost("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> UpdateResume(Guid id, [FromBody] UpdateResumeRequestDto request)
        {
            if (id != request.Id)
            {
                return BadRequest("ID in the URL does not match ID in the request body");
            }

            var userId = _currentUserService.UserId;

            // Verify the resume belongs to the current user or user is admin/staff
            var resumeResult = await Mediator.Send(new GetResumeByIdQuery { Id = id });
            if (resumeResult.IsFailed)
            {
                return NotFound(new { Errors = resumeResult.Errors });
            }

            var resume = resumeResult.Value;
            if (resume.UserId != userId && !User.IsInRole("Admin") && !User.IsInRole("Staff"))
            {
                return Forbid();
            }
            var workExperiences = _mapper.Map<List<WorkExperience>>(request.WorkExperiences);
            var educations = _mapper.Map<List<Education>>(request.Educations);
            var skills = _mapper.Map<List<Skill>>(request.Skills);
            var languages = _mapper.Map<List<Language>>(request.Languages);
            var command = new UpdateResumeCommand
            {
                Id = id,
                Resume = new Resume()
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Address = request.Address,
                    ProfilePictureUrl = request.ProfilePictureUrl,
                    Summary = request.Summary,
                    WorkExperiences = workExperiences,
                    Educations = educations,
                    Skills = skills,
                    Languages = languages
                }
            };

            var result = await Mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a resume
        /// </summary>
        /// <param name="id">The ID of the resume to delete</param>
        /// <returns>No content if successful</returns>
        [HttpPost("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = Roles.AdminAndUser)]
        public async Task<IActionResult> DeleteResume(Guid id)
        {
            var userId = _currentUserService.UserId;

            // Verify the resume exists and belongs to the current user or user is admin/staff
            var resumeResult = await Mediator.Send(new GetResumeByIdQuery { Id = id });
            if (resumeResult.IsFailed)
            {
                return NotFound(new { Errors = resumeResult.Errors });
            }

            var resume = resumeResult.Value;
            if (resume.UserId != userId && !User.IsInRole("Admin") && !User.IsInRole("Staff"))
            {
                return Forbid();
            }

            var command = new DeleteResumeCommand(id);
            var result = await Mediator.Send(command);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
