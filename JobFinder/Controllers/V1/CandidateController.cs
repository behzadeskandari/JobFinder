using AutoMapper;
using JobFinder.Application.Feature.Candidate.Command.CreateCandidateCommand;
using JobFinder.Application.Feature.Candidate.Command.DeleteCandidateCommand;
using JobFinder.Application.Feature.Candidate.Command.UpdateCandidateCommand;
using JobFinder.Application.Feature.Candidate.Queries.DownloadPdfFileQuery;
using JobFinder.Application.Feature.Candidate.Queries.GetCandidateByIdQuery;
using JobFinder.Application.Feature.Candidate.Queries.GetCandidateResumesQuery;
using JobFinder.Application.Feature.Candidate.Queries.GetCandidatesQuery;
using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Contracts.Dtos.Resume;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers.V1
{
    public class CandidateController : ApiController
    {
        private readonly ILogger<CandidateController> _logger;
        public CandidateController(ILogger<CandidateController> logger, IMapper mapper)
        {
            _logger = logger;
        }

        // Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCandidate([FromForm] CandidateCreateDto dto, IFormFile pdfFile)
        {
            try
            {
                var result = await Mediator.Send(new CreateCandidateCommand { CandidateDto = dto, PdfFile = pdfFile });

                if (result.IsSuccess)
                {
                    return Ok(result.Successes);
                }
                else
                {
                    return Problem(result.Errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating candidate");
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "Internal server error");
            }
        }

        // Read all candidates
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<CandidateGetDto>>> GetCandidates()
        {
            try
            {
                var result = await Mediator.Send(new GetCandidatesQuery());

                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                }
                else
                {
                    return Problem(statusCode: StatusCodes.Status404NotFound, detail: result.Errors.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving candidates");
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "Internal server error");
            }
        }

        // Read (Download Pdf File)
        [HttpGet("download/{url}")]
        public async Task<IActionResult> DownloadPdfFile(string url)
        {

            try
            {
                var result = await Mediator.Send(new DownloadPdfFileQuery { Url = url });

                if (result.IsSuccess)
                {
                    return result.Value;
                }
                else
                {
                    return Problem(statusCode: StatusCodes.Status204NoContent, detail: result.Errors.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error downloading file {url}");
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "Internal server error");
            }

        }

        // Read (Get Candidate By ID)
        [HttpGet("GetCandidate/{id}")]
        public async Task<ActionResult<CandidateGetDto>> GetCandidate(Guid id)
        {
            try
            {
                var result = await Mediator.Send(new GetCandidateByIdQuery { Id = id });

                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                }
                else
                {
                    return Problem(statusCode: StatusCodes.Status204NoContent, detail: result.Errors.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving candidate with ID {id}");
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "Internal server error");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateCandidate/{id}")]
        public async Task<IActionResult> UpdateCandidate(Guid id, [FromBody] CandidateUpdateDto dto)
        {
            try
            {
                var command = new UpdateCandidateCommand { Id = id, CandidateDto = dto };
                var result = await Mediator.Send(command);

                if (result.IsSuccess)
                {
                    return Ok(result.Successes);
                }
                else
                {
                    return Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating candidate with ID {id}");
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "Internal server error");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DeleteCandidate/{id}")]
        public async Task<IActionResult> DeleteCandidate(Guid id)
        {
            try
            {
                var command = new DeleteCandidateCommand { Id = id };
                var result = await Mediator.Send(command);

                if (result.IsSuccess)
                {
                    return Ok(result.Successes);
                }
                else
                {
                    return Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting candidate with ID {id}");
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "Internal server error");
            }
        }

        [HttpGet("{candidateId}/resumes")]
        public async Task<ActionResult<IEnumerable<ResumeDto>>> GetCandidateResumes(int candidateId)
        {
            var result = await Mediator.Send(new GetCandidateResumesQuery { CandidateId = candidateId });
            return result.IsSuccess ? Ok(result.Value) : Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.ToString());
        }


    }
}
