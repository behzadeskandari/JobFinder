using Domain.Roles;
using JobFinder.Application.Feature.DropDowns.Cities.Command;
using JobFinder.Application.Feature.DropDowns.Cities.Queries;
using JobFinder.Contracts.Dtos.Cities;
using JobFinder.Contracts.Dtos.DropDown;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class CitiesController : ApiController
    {
        /// <summary>
        /// GetAllCities
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCities")]
        public async Task<ActionResult<List<CityDto>>> GetAllCities()
        {
            var query = new GetAllCitiesQuery();
            var cities = await Mediator.Send(query);
            if (cities.IsSuccess)
            {
                return Ok(cities);

            }
            else
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: cities.Errors.ToString());
                //return BadRequest(cities.Errors);
            }
        }



        [HttpGet("GetCityById/{id}")]
        public async Task<IActionResult> GetCityById(int id)
        {
            var result = await Mediator.Send(new GetCityById { ProvinceId = id });

            if (result.IsSuccess && result.Value.Count > 0)
            {
                return Ok(result.Value);
            }
            else
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: result.Errors.ToString());
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities([FromQuery] int? provinceId)
        {
            var result = await Mediator.Send(new GetCitiesQuery { ProvinceId = provinceId });
            return result.IsSuccess ? Ok(result.Value) :
            Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
        }

        /// <summary>
        /// UpdateCities City
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost("UpdateCities")]
        public async Task<IActionResult> Update([FromBody] UpdateCityDto dto)
        {
            var result = await Mediator.Send(new UpdateCityCommand(dto));
            return result.IsSuccess ? Ok() :
            Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
        }
        /// <summary>
        /// Create City
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCityDto dto)
        {
            var result = await Mediator.Send(new CreateCityCommand(dto));
            return result.IsSuccess ? Ok() :
                 Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
        }

        /// <summary>
        /// DeleteCities City
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost("DeleteCities/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteCityCommand(id));
            return result.IsSuccess ? Ok() : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
        }
    }
}
