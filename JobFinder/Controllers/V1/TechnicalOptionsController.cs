using JobFinder.Application.Feature.DropDowns.TechnicalOptions.Command;
using JobFinder.Application.Feature.DropDowns.TechnicalOptions.Queries;
using JobFinder.Contracts.Dtos.DropDown;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class TechnicalOptionsController : ApiController
    {
        [HttpGet("GetTechnicalOptionsTechnical")]
        public async Task<ActionResult<IEnumerable<TechnicalOptionDto>>> GetTechnicalOptions()
        {
            var result = await Mediator.Send(new GetTechnicalOptionsQuery());

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString()); ;
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetTechnicalOptionByIdQuery { Id = id });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateTechnicalOptionDto dto)
        {
            var result = await Mediator.Send(new CreateTechnicalOptionCommand(dto));
            return result.IsSuccess ? Ok() : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());//BadRequest(result.Errors);
        }

        [HttpPost("(UpdateTechnicalOption")]
        public async Task<IActionResult> Update(UpdateTechnicalOptionDto dto)
        {
            var result = await Mediator.Send(new UpdateTechnicalOptionCommand(dto));
            return result.IsSuccess ? Ok() : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());//BadRequest(result.Errors);
        }

        [HttpPost("DeleteTechnicalOption/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteTechnicalOptionCommand(id));
            return result.IsSuccess ? Ok() : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
        }
    }

}
