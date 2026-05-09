using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Candidate.Queries.DownloadPdfFileQuery;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Handlers.DownloadPdfFileQueryHandler
{
    public class DownloadPdfFileQueryHandler : IRequestHandler<DownloadPdfFileQuery, Result<FileStreamResult>>
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DownloadPdfFileQueryHandler> _logger;
        public DownloadPdfFileQueryHandler(IWebHostEnvironment environment, IUnitOfWork unitOfWork, ILogger<DownloadPdfFileQueryHandler> logger)
        {
            _environment = environment;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<FileStreamResult>> Handle(DownloadPdfFileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.CandidateRepository.GetPdfFile(request.Url);

                if (result.IsSuccess)
                {
                    var fileDownloadDto = result.Value;
                    var fileStreamResult = new FileStreamResult(fileDownloadDto.FileStream, "application/pdf")
                    {
                        FileDownloadName = fileDownloadDto.FileDownloadName,
                        EnableRangeProcessing = fileDownloadDto.EnableRangeProcessing
                    };

                    return Result.Ok(fileStreamResult);
                }
                else
                {
                    return Result.Fail<FileStreamResult>(result.Errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error handling file download for {request.Url}");
                return Result.Fail<FileStreamResult>("Internal server error").WithError(ex.Message);
            }
        }
    }
}
