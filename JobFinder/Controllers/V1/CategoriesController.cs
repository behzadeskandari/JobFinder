using Domain.Roles;
using JobFinder.Application.Feature.Categories.Commands.CreateCategoryCommand;
using JobFinder.Application.Feature.Categories.Commands.DeleteCategoryCommand;
using JobFinder.Application.Feature.Categories.Commands.UpdateCategoryCommand;
using JobFinder.Application.Feature.Categories.Queries.GetCategories;
using JobFinder.Application.Feature.Categories.Queries.GetCategoryByIdQuery;
using JobFinder.Contracts.Dtos.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class CategoriesController : ApiController
    {
        [Authorize(Roles =Roles.All)]
        [HttpGet("GetCategories")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            return await Mediator.Send(new GetCategoriesQuery());
        }

        [Authorize(Roles = Roles.All)]
        [HttpGet("GetCategory/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            return await Mediator.Send(new GetCategoryByIdQuery { Id = id });
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("CreateCategory")]
        //    [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            return await Mediator.Send(new CreateCategoryCommand { Category = createCategoryDto });
        }


        [Authorize(Roles = Roles.Admin)]
        [HttpPost("UpdateCategory/{id}")]
        //     [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            await Mediator.Send(new UpdateCategoryCommand { Id = id, Category = updateCategoryDto });
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");

        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("DeleteCategory/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }
}
