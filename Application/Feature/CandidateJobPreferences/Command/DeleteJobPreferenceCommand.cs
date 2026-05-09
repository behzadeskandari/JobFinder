using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.CandidateJobPreferences.Command
{
    public class DeleteJobPreferenceCommand : MediatR.IRequest<Result>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
