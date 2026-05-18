using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class CandidateJobPreferencesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CandidateJobPreferencesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidateJobPreferences()
        {
            var candidateJobPreferences = await _unitOfWork.candidateJobPreferences.GetAllAsync();
            return Ok(candidateJobPreferences);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateJobPreference(int id)
        {
            var candidateJobPreference = await _unitOfWork.candidateJobPreferences.GetByIdAsync(id);
            if (candidateJobPreference == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NOT Found");
            }
            return Ok(candidateJobPreference);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidateJobPreference([FromBody] CandidateJobPreferenceDto candidateJobPreference)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var _userName = userName;
            var _userId = userId;
            if (string.IsNullOrEmpty(userId))
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "User Not Signed In");
            }
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(_userId);
            if (user == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "User Not Found either Log in or register and login");
            }
            var jobCat = await _unitOfWork.JobCategoryRepository.GetByIdAsync(candidateJobPreference.JobCategoryId);
            if (jobCat == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "jobCategory Not Found");
            }

            var City = await _unitOfWork.CitiesRepository.GetByIdAsync(candidateJobPreference.CityId);
            if (City == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "City Not Found");
            }

            CandidateJobPreferences candidateJobPref = new CandidateJobPreferences();
            candidateJobPref.JobCategory = jobCat;
            candidateJobPref.JobCategoryId = jobCat.Id;
            candidateJobPref.PreferredCityId = City.Id;
            candidateJobPref.City = City;
            candidateJobPref.ExpectedSalary = candidateJobPreference.ExpectedSalary;
            candidateJobPref.PreferredIndustry = candidateJobPreference.PreferredIndustry;
            candidateJobPref.PreferredLocation = candidateJobPreference.PreferredLocation;
            candidateJobPref.JobType = candidateJobPreference.JobType;
            candidateJobPref.User = user;
            candidateJobPref.UserId = user.Id;
            candidateJobPref.PreferredJobType = candidateJobPreference.PreferredJobType;
            candidateJobPref.MinSalary = candidateJobPreference.MinSalary;

            await _unitOfWork.candidateJobPreferences.AddAsync(candidateJobPref);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetCandidateJobPreference), new { id = candidateJobPreference.Id }, candidateJobPreference);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateCandidateJobPreference(Guid id, [FromBody] CandidateJobPreferences candidateJobPreference)
        {
            if (!ModelState.IsValid || id != candidateJobPreference.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Bad Request");
            }

            var existingCandidateJobPreference = await _unitOfWork.candidateJobPreferences.GetByIdAsync(id);
            if (existingCandidateJobPreference == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NOT Found");
            }

            await _unitOfWork.candidateJobPreferences.UpdateAsync(candidateJobPreference);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NO Cotext"); ;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteCandidateJobPreference(int id)
        {
            var candidateJobPreference = await _unitOfWork.candidateJobPreferences.GetByIdAsync(id);
            if (candidateJobPreference == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NOT Found");
            }

            await _unitOfWork.candidateJobPreferences.DeleteAsync(candidateJobPreference);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NO Cotext");
        }
    }

}
