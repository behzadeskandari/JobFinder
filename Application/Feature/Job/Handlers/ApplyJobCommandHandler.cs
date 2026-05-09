using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Job.Command;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Job.Handlers
{
    public class ApplyJobCommandHandler : IRequestHandler<ApplyJobCommand, Result<JobApplicationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplyJobCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<JobApplicationDto>> Handle(ApplyJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _unitOfWork.JobApplication
                .GetQueryable()
                .FirstOrDefaultAsync(x=> x.Id == request.JobId, cancellationToken);

            if (job == null || job.IsActive != true)
                throw new NotFoundException("شغل یافت نشد یا غیرفعال است");

            var candidate = await _unitOfWork.JobApplication
                .GetQueryable()
                .FirstOrDefaultAsync(c => c.CandidateId == request.CandidateId,cancellationToken);

            if (candidate == null)
                throw new NotFoundException("کاندیدا پیدا نشد");

            var existingApplication = await _unitOfWork.JobApplication.GetQueryable()
                .FirstOrDefaultAsync(ja => ja.JobId == request.JobId && ja.CandidateId == candidate.Id,cancellationToken);

            if (existingApplication != null)
                throw new NotFoundException("قبلاً برای این شغل درخواست داده‌اید");

            var jobApplication = new JobApplication
            {
                JobId = request.JobId,
                CandidateId = candidate.Id,
                ApplicationDate = DateTime.Now,
                ResumeFileName = request.ResumeFileName,
                ResumeFileUrl = request.ResumeFileUrl,
                CoverLetter = request.CoverLetter,
                Status = "Submitted",
                Notes = "",
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _unitOfWork.JobApplication.AddAsync(jobApplication);
            await _unitOfWork.CommitAsync();

            var jobApplicationDto = _mapper.Map<JobApplicationDto>(jobApplication);
            return Result.Ok(jobApplicationDto);
        }
    }
}
