using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Roles;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobPosts.Commands.CreateJobPost;
using JobFinder.Application.Feature.JobPosts.Commands.DeleteJobPost;
using JobFinder.Application.Feature.JobPosts.Commands.UpdateJobPost;
using JobFinder.Application.Feature.JobPosts.Queries.GetJobPostById;
using JobFinder.Application.Feature.JobPosts.Queries.GetJobPostsQuery;
using JobFinder.Contracts.Dtos.JobPost;
using JobFinder.Domain.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Controllers.V1
{
    public class JobPostsController : ApiController
    {
        private readonly IAccountService _accountService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public JobPostsController(IAccountService accountService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _accountService = accountService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<PaginatedList<JobPostDto>>> GetJobPosts([FromQuery] GetJobPostsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("GetJobPost/{id}")]
        public async Task<ActionResult<JobPostDto>> GetJobPost(Guid id)
        {
            return await Mediator.Send(new GetJobPostByIdQuery { Id = id });
        }

        [Authorize(Roles = Roles.StaffAndAbove)]
        [HttpPost("CreateJobPost")]
        public async Task<ActionResult<Guid>> CreateJobPost(CreateJobPostDto createJobPostDto)
        {
            // Get the current user's ID.
            var user = await _accountService.GetUserAsync(User);
            if (user == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "USer Not Found");
            }
            return await Mediator.Send(new CreateJobPostCommand { JobPost = createJobPostDto });
        }

        [HttpPost("UpdateJobPost/{id}")]
        [Authorize(Roles = Roles.StaffAndAbove)]
        public async Task<ActionResult> UpdateJobPost(Guid id, UpdateJobPostDto updateJobPostDto)
        {

            await Mediator.Send(new UpdateJobPostCommand { Id = id, JobPost = updateJobPostDto });
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("DeleteJobPost/{id}")]
        [Authorize(Roles = Roles.StaffAndAbove)]
        public async Task<ActionResult> DeleteJobPost(Guid id)
        {
            await Mediator.Send(new DeleteJobPostCommand { Id = id });
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpGet("GetJobPostForCompany")]
        public async Task<ActionResult<List<JobPostDto>>> GetJobPostForCompany()
        {
            var loggedInUserId = userId;

            if (loggedInUserId == null)
            {
                return NotFound("کاربری پیدا نشد لطفا لاگین کنید.");
            }
            var company = await _unitOfWork.companyRepository.GetQueryable().FirstOrDefaultAsync(x => x.UserId == loggedInUserId);

            if (company == null)
            {
                return NotFound("شرکتی برای این کاربر یافت نشد.");
            }

            // گرفتن پست‌های شغلی مرتبط با این شرکت
            var jobPosts = await _unitOfWork.JobsRepository
                .GetQueryable()
                .Where(jp => jp.CompanyId == company.Id)
                .ProjectTo<JobPostDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return jobPosts;
        }
    }
}
