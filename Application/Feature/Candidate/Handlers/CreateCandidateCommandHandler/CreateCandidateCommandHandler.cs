using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Candidate.Command.CreateCandidateCommand;
using JobFinder.Contracts.Dtos.Candidate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Handlers.CreateCandidateCommandHandler
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCandidateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var maxFileSizeBytes = 5 * 1024 * 1024; // 5 MB
            var pdfMimeType = "application/pdf";

            if (request.PdfFile.Length > maxFileSizeBytes || request.PdfFile.ContentType != pdfMimeType)
            {
                throw new NotFoundException("فایل معتبر نیست");
            }

            var resumeFileName = Guid.NewGuid().ToString() + ".pdf";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs", resumeFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.PdfFile.CopyToAsync(stream);
            }
            var candidate = new CandidateDto() {
                CoverLetter = request.CandidateDto.CoverLetter,
                Email = request.CandidateDto.Email,
                FirstName = request.CandidateDto.FirstName,
                LastName = request.CandidateDto.LastName,
                Phone = request.CandidateDto.Phone,
            };


            var result  = await _unitOfWork.CandidateRepository.CreateCandidate(candidate, resumeFileName);
            
            return result;
        }
    }
}
