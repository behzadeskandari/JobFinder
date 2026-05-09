using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.SavedJobs;

namespace JobFinder.Application.Feature.SavedJobs.Commands
{
    public class CreateSavedJobCommand : MediatR.IRequest<Result<SavedJobDto>>
    {
        public Guid JobId { get; set; }
        public string UserId { get; set; }
    }
}
