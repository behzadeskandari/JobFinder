using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.SavedJobs;

namespace JobFinder.Application.Feature.SavedJobs.Query
{
    public class GetSavedJobByIdQuery : MediatR.IRequest<Result<SavedJobDto>>
    {
        public int Id { get; set; }
    }
}
