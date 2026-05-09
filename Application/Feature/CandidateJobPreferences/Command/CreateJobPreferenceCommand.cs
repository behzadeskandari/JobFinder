using FluentResults;
using JobFinder.Contracts.Dtos.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.CandidateJobPreferences.Command
{
    public class CreateJobPreferenceCommand : MediatR.IRequest<Result<CandidateJobPreferenceDto>>
    {
        public string UserId { get; set; }
        public int? JobCategoryId { get; set; }
        public int? CityId { get; set; }
        public string JobType { get; set; }
        public decimal? MinSalary { get; set; }
    }
}
