using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Queries.DownloadPdfFileQuery
{
    public class DownloadPdfFileQuery : IRequest<Result<FileStreamResult>>
    {
        public string Url { get; set; }
    }
}
