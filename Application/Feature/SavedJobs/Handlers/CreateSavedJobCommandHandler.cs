using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.SavedJobs.Commands;
using JobFinder.Contracts.Dtos.SavedJobs;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.SavedJobs.Handlers
{
    public class CreateSavedJobCommandHandler : IRequestHandler<CreateSavedJobCommand, Result<SavedJobDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSavedJobCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<SavedJobDto>> Handle(CreateSavedJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _unitOfWork.JobsRepository.GetQueryable().FirstOrDefaultAsync(x=> x.Id == request.JobId , cancellationToken);
            if (job == null || job.IsActive != true)
                throw new NotFoundException("شغل پیدا نشد");

            var user = await _unitOfWork.UsersRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == request.UserId,cancellationToken);
            if (user == null)
                throw new NotFoundException("کاربر پیدا نشد");

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
