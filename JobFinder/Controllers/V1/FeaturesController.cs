using JobFinder.Application.Feature.Pricing.Queries.GetFeature;
using JobFinder.Contracts.Dtos.Feature;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class FeaturesController : ApiController
    {
        [HttpGet("GetFeatures")]
        public async Task<ActionResult<IEnumerable<FeatureDto>>> GetFeatures()
        {
            var result = await Mediator.Send(new GetFeaturesQuery());
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(result);
        }



    }
}
