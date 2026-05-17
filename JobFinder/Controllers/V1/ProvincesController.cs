using Domain.Roles;
using JobFinder.Application.Feature.DropDowns.Province.Command;
using JobFinder.Application.Feature.DropDowns.Province.Queries;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Contracts.Dtos.Province;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class ProvincesController : ApiController
    {

        [Authorize(Roles = Roles.All)]
        [HttpGet("GetAllProvinces")]
        public async Task<ActionResult<List<ProvinceDto>>> GetAllProvinces()
        {
            var query = new GetAllProvincesQuery();
            var provinces = await Mediator.Send(query);
            if (provinces.IsSuccess)
            {
                return Ok(provinces);
            }
            else
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: provinces.Errors.ToString());
            }
        }

        [Authorize(Roles = Roles.All)]
        [HttpGet("GetProvinceById/{id}")]
        public async Task<ActionResult<List<ProvinceDto>>> GetProvinceById(int id)
        {
            var query = new GetProvinceById();
            query.Id = id;
            var provinces = await Mediator.Send(query);
            if (provinces.IsSuccess)
            {
                return Ok(provinces);
            }
            else
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: provinces.Errors.ToString());
            }

        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateProvinceDto dto)
        {
            var result = await Mediator.Send(new CreateProvinceCommand(dto));
            return result.IsSuccess ? Ok() : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateProvinceDto dto)
        {
            var result = await Mediator.Send(new UpdateProvinceCommand(dto));
            return result.IsSuccess ? Ok() :
                Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
            //BadRequest(result.Errors);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteProvinceCommand(id));
            return result.IsSuccess ? Ok() : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
        }

    }
}
