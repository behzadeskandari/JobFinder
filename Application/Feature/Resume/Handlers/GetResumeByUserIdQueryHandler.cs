using System;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Resume.Queries;
using JobFinder.Contracts.Dtos.Resume;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Resume.Handlers
{
    public class GetResumeByUserIdQueryHandler : IRequestHandler<GetResumeByUserIdQuery, Result<ResumeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetResumeByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<ResumeDto>> Handle(GetResumeByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resume = await _unitOfWork.ResumeRepository
                    .GetQueryable()
                    .Include(r => r.WorkExperiences)
                    .Include(r => r.Educations)
                    .Include(r => r.Skills)
                    .Include(r => r.Languages)
                    .FirstOrDefaultAsync(r => r.UserId == request.UserId, cancellationToken);

                if (resume == null)
                {
                    return Result.Fail<ResumeDto>("Resume not found for the specified user");
                }

                var resumeDto = new ResumeDto
                {
                    UserId = resume.UserId,
                    FullName = resume.FullName,
                    Email = resume.Email,
                    Phone = resume.Phone,
                    Address = resume.Address,
                    ProfilePictureUrl = resume.ProfilePictureUrl,
                    Summary = resume.Summary,
                    // Map other properties as needed
                };

                return Result.Ok(resumeDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<ResumeDto>(new Error("Failed to retrieve resume").CausedBy(ex));
            }
        }
    }
}
