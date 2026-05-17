using JobFinder.Application.Feature.TermsOfService.Queries;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{

    public class TermsOfServiceController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<TermsOfService>>> GetAllTermsOfServices()
        {
            var query = new GetAllTermsOfServicesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<TermsOfService>> GetTermsOfServiceById(int id)
        {
            var query = new GetTermsOfServiceByIdQuery(id);
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(result);
        }
    }
}
