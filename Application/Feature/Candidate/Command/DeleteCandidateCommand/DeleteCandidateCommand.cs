using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Command.DeleteCandidateCommand
{
    public class DeleteCandidateCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }
}
