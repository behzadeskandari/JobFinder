using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class JobTestAssignmentsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobTestAssignmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobTestAssignments()
        {
            var jobTestAssignments = await _unitOfWork.jobTestAssignment.GetAllAsync();
            return Ok(jobTestAssignments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobTestAssignment(int id)
        {
            var jobTestAssignment = await _unitOfWork.jobTestAssignment.GetByIdAsync(id);
            if (jobTestAssignment == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(jobTestAssignment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobTestAssignment([FromBody] JobTestAssignment jobTestAssignment)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            await _unitOfWork.jobTestAssignment.AddAsync(jobTestAssignment);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetJobTestAssignment), new { id = jobTestAssignment.Id }, jobTestAssignment);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateJobTestAssignment(Guid id, [FromBody] JobTestAssignment jobTestAssignment)
        {
            if (!ModelState.IsValid || id != jobTestAssignment.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingJobTestAssignment = await _unitOfWork.jobTestAssignment.GetByIdAsync(id);
            if (existingJobTestAssignment == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.jobTestAssignment.UpdateAsync(jobTestAssignment);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteJobTestAssignment(int id)
        {
            var jobTestAssignment = await _unitOfWork.jobTestAssignment.GetByIdAsync(id);
            if (jobTestAssignment == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.jobTestAssignment.DeleteAsync(jobTestAssignment);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }

}
