using JobFinder.Application.Feature.Benefits.Command;
using JobFinder.Application.Feature.Benefits.Queries;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class CompanyBenefitsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<CompanyBenefit>>> GetAllCompanyBenefits()
        {
            var query = new GetAllCompanyBenefitsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyBenefit>> GetCompanyBenefitById(Guid id)
        {
            var query = new GetCompanyBenefitByIdQuery(id);
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");

            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCompanyBenefit(CreateCompanyBenefitCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetCompanyBenefitById), new { id = result }, result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateCompanyBenefit(Guid id, UpdateCompanyBenefitCommand command)
        {
            if (id != command.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Status400BadRequest");
            }
            var result = await Mediator.Send(command);
            if (!result)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Status404NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "Status204NoContent"); ;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteCompanyBenefit(int id)
        {
            var command = new DeleteCompanyBenefitCommand(id);
            var result = await Mediator.Send(command);
            if (!result)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Status404NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "Status204NoContent");
        }
    }

}
