using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Job;

namespace JobFinder.Application.Feature.CandidateJobPreferences.Queries
{
    public class GetJobPreferencesQuery : MediatR.IRequest<Result<IEnumerable<CandidateJobPreferenceDto>>>
    {
        public string UserId { get; set; }
    }
}
