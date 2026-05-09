using FluentResults;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Feature.Resume.Queries;
using JobFinder.Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Common.Exceptions;

namespace JobFinder.Application.Feature.Resume.Handlers
{
    public class GetResumePdfQueryHandler : IRequestHandler<GetResumePdfQuery, Result<byte[]>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPdfService _pdfService;

        public GetResumePdfQueryHandler(IUnitOfWork unitOfWork, IPdfService pdfService)
        {
            _unitOfWork = unitOfWork;
            _pdfService = pdfService;
        }

        public async Task<Result<byte[]>> Handle(GetResumePdfQuery request, CancellationToken cancellationToken)
        {
            var resume = await _unitOfWork.ResumeRepository.GetResume(request.Id);
            if (resume == null)
            {
                throw new NotFoundException("رزومه مورد نظر پیدا نشد");
            }

            var pdfBytes = await _pdfService.GenerateResumePdf(resume);
            return new Result<byte[]>().WithSuccess(" Created SuccessFully").WithValue(pdfBytes);
        }
    }
}
