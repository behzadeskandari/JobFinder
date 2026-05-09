using FluentResults;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Resume;

namespace JobFinder.Application.Feature.Resume.Queries
{
    public class GetResumeByIdQuery : IRequest<Result<ResumeDto>>
    {
        public Guid Id { get; set; }
    }


}
