using FluentResults;
using JobFinder.Contracts.Dtos.Resume;
using MediatR;

namespace JobFinder.Application.Feature.Resume.Queries
{
    public class GetResumeByUserIdQuery : IRequest<Result<ResumeDto>>
    {
        public string UserId { get; set; }
    }
}
