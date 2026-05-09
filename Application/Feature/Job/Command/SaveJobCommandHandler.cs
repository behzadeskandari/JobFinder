using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Contracts.Dtos.SavedJobs;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Job.Command
{
    public class SaveJobCommandHandler : IRequestHandler<SaveJobCommand, Result<SavedJobDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveJobCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<SavedJobDto>> Handle(SaveJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _unitOfWork.JobsRepository.GetQueryable().FirstOrDefaultAsync(x=> x.Id == request.JobId,cancellationToken);
            if (job == null || job.IsActive != true)
                return Result.Fail("Job not found or inactive");

            var user = await _unitOfWork.UsersRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return Result.Fail("User not found");

            var existingSavedJob = await _unitOfWork.SavedJob
                .FindAsync(sj => sj.JobId == request.JobId && sj.UserId == request.UserId && sj.IsActive == true);
                

            if (existingSavedJob != null)
                return Result.Fail("Job already saved");

            var savedJob = new SavedJob
            {
                JobId = request.JobId,
                UserId = request.UserId,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _unitOfWork.SavedJob.AddAsync(savedJob);
            await _unitOfWork.CommitAsync();

            var savedJobDto = _mapper.Map<SavedJobDto>(savedJob);
            return Result.Ok(savedJobDto);
        }
    }
}
