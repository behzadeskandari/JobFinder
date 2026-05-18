using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class CompanyJobPreferencesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyJobPreferencesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyJobPreferences()
        {
            var companyJobPreferences = await _unitOfWork.companyJobPreferences.GetAllAsync();
            return Ok(companyJobPreferences);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyJobPreference(int id)
        {
            var companyJobPreference = await _unitOfWork.companyJobPreferences.GetByIdAsync(id);
            if (companyJobPreference == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(companyJobPreference);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyJobPreference([FromBody] CompanyJobPreferences companyJobPreference)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            await _unitOfWork.companyJobPreferences.AddAsync(companyJobPreference);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetCompanyJobPreference), new { id = companyJobPreference.Id }, companyJobPreference);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateCompanyJobPreference(Guid id, [FromBody] CompanyJobPreferences companyJobPreference)
        {
            if (!ModelState.IsValid || id != companyJobPreference.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingCompanyJobPreference = await _unitOfWork.companyJobPreferences.GetByIdAsync(id);
            if (existingCompanyJobPreference == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.companyJobPreferences.UpdateAsync(companyJobPreference);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteCompanyJobPreference(int id)
        {
            var companyJobPreference = await _unitOfWork.companyJobPreferences.GetByIdAsync(id);
            if (companyJobPreference == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.companyJobPreferences.DeleteAsync(companyJobPreference);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }

}
