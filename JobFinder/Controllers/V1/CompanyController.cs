using System.Net;
using AutoMapper;
using Domain.Response;
using Domain.Roles;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Companies.Command.CreateCompanyCommand;
using JobFinder.Application.Feature.Companies.Command.DeleteCompanyCommand;
using JobFinder.Application.Feature.Companies.Command.UpdateCompanyCommand;
using JobFinder.Application.Feature.Companies.Queries.GetAllCompaniesQuery;
using JobFinder.Application.Feature.Companies.Queries.GetAllIndustriesQuery;
using JobFinder.Application.Feature.Companies.Queries.GetAllLocationsQuery;
using JobFinder.Application.Feature.Companies.Queries.GetCompaniesQuery;
using JobFinder.Application.Feature.Companies.Queries.GetCompanyByIdQuery;
using JobFinder.Application.Feature.Companies.Queries.GetCompanyJobsQuery;
using JobFinder.Application.Feature.Companies.Queries.SearchCompaniesQuery;
using JobFinder.Application.Feature.CompanyFollows.Command;
using JobFinder.Application.Feature.CompanyFollows.Queries;
using JobFinder.Contracts.Dtos.Company;
using JobFinder.Contracts.Dtos.CompanyFollows;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class CompanyController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }



        [Authorize(Roles = Roles.Admin)]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCompaniesQuery();
            var result = await Mediator.Send(query);
            Response<IEnumerable<Company>> res = new Response<IEnumerable<Company>>();

            res.Items = result;
            res.StatusCode = HttpStatusCode.Accepted;
            res.Message = "Sucseess";
            res.Count = result.Count();
            return Ok(res);
        }
        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term, [FromQuery] string industry, [FromQuery] string location)
        {
            var query = new SearchCompaniesQuery
            {
                SearchTerm = term,
                Industry = industry,
                Location = location
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [Authorize(Roles = Roles.Admin)]
        [HttpGet("industries")]
        public async Task<IActionResult> GetAllIndustries()
        {
            var query = new GetAllIndustriesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("locations")]
        public async Task<IActionResult> GetAllLocations()
        {
            var query = new GetAllLocationsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [Authorize(Roles = Roles.Admin)]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetCompanyByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            if (result == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Status404NotFound");

            return Ok(result);
        }


        [Authorize(Roles = Roles.StaffAndAbove)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyCommand command)
        {
            if (id != command.Id)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");

            var result = await Mediator.Send(command);

            if (!result)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Status404NotFound");

            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "Status204NoContent");
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCompanyCommand { Id = id };
            var result = await Mediator.Send(command);

            if (!result)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Status404NotFound");

            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "Status204NoContent");
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveCompany(int id)
        {
            var company = await _unitOfWork.companyRepository.GetByIdAsync(id);
            if (company == null)
                return NotFound();

            company.IsActive = true;
            company.IsVerified = true;
            await _unitOfWork.companyRepository.UpdateAsync(company);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "Status204NoContent");
        }

        [HttpGet]
        [Authorize(Roles = Roles.All)]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies([FromQuery] SearchCompaniesQueryDto searchCriteria)
        {
            var result = await Mediator.Send(new GetCompaniesQuery { SearchCriteria = searchCriteria });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
        }


        [HttpGet("followed")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetFollowedCompanies()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await Mediator.Send(new GetFollowedCompaniesQuery { UserId = userId });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
        }

        [HttpPost("follow/{companyId}")]
        [Authorize]
        public async Task<ActionResult<CompanyFollowDto>> FollowCompany(Guid companyId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await Mediator.Send(new FollowCompanyCommand { CompanyId = companyId, UserId = userId });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
        }


        [HttpPost("follow/{companyId}")]
        [Authorize]
        public async Task<ActionResult> UnfollowCompany(Guid companyId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await Mediator.Send(new UnfollowCompanyCommand { CompanyId = companyId, UserId = userId });
            return result.IsSuccess ? NoContent() : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
        }

        [HttpGet("top-rated")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetTopRatedCompanies()
        {
            var result = await Mediator.Send(new GetTopRatedCompaniesQuery());
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
        }

        [HttpGet("filters")]
        [AllowAnonymous]
        public async Task<ActionResult<CompanyFiltersDto>> GetCompanyFilters()
        {
            var result = await Mediator.Send(new GetCompanyFiltersQuery());
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
        }

        [HttpGet("{companyId}/jobs")]
        public async Task<ActionResult<PagedResult<JobGetDto>>> GetCompanyJobs(Guid companyId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await Mediator.Send(new GetCompanyJobsQuery { CompanyId = companyId, PageNumber = pageNumber, PageSize = pageSize });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
        }



        [HttpGet("GetCurrentCompany")]
        public async Task<ActionResult<CompanyDto>> GetCurrentCompany()
        {
            var user = userId;
            if (user == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "user not found");
            }
            var company = _unitOfWork.companyRepository.GetQueryable().FirstOrDefault(x => x.UserId == user);
            if (company == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "No Company registered");
            }

            return Ok(company);
        }
    }
}
