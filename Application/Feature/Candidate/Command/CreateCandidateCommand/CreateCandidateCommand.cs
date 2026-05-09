using FluentResults;
using JobFinder.Contracts.Dtos.Candidate;
using JobFinder.Contracts.Utility;
using JobFinder.Infrastructure.Utility;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Command.CreateCandidateCommand
{
    public class CreateCandidateCommand : IRequest<Result>
    {
        public CandidateCreateDto CandidateDto { get; set; }
        [MaxFilesSize(5)]
        [AllowedExtension(new string[] { ".pdf", ".docx", ".doc" })]
        public IFormFile PdfFile { get; set; }
    }
}
