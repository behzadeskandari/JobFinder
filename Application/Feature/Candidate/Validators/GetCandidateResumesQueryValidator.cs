using FluentValidation;
using JobFinder.Application.Feature.Candidate.Queries.GetCandidateResumesQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Validators
{
    public class GetCandidateResumesQueryValidator : AbstractValidator<GetCandidateResumesQuery>
    {
        public GetCandidateResumesQueryValidator()
        {
            RuleFor(x => x.CandidateId).GreaterThan(0).WithMessage("Candidate ID is required");
        }
    }
}
