using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.Job;

namespace JobFinder.Application.Feature.Job.Command
{
    public class ApplyJobCommand : MediatR.IRequest<Result<JobApplicationDto>>
    {
        public Guid JobId { get; set; }
        public string ResumeFileName { get; set; }
        public string ResumeFileUrl { get; set; }
        public string CoverLetter { get; set; }
        public Guid CandidateId { get; set; }
        public string UserId { get; set; }


    }
}
