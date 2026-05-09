using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.SavedJobs.Commands;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.SavedJobs.Handlers
{
    public class UpdateSavedJobCommandHandler : IRequestHandler<UpdateSavedJobCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSavedJobCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateSavedJobCommand request, CancellationToken cancellationToken)
        {
            var savedJob = await _unitOfWork.SavedJob
                .GetQueryable()
                .FirstOrDefaultAsync(x=> x.Id == request.Id, cancellationToken);

            if (savedJob == null)
                throw new NotFoundException("شغل ذخیره شده پیدا نشد");

            var job = await _unitOfWork.JobsRepository
                .GetByIdAsync(request.JobId);

            if (job == null || job.IsActive != true)
                throw new NotFoundException("شغل پیدا نشد و یا غیر فعال است");

            var user = await _unitOfWork.UsersRepository.GetByIdAsync(request.UserId);

            if (user == null)
                throw new NotFoundException("کابر پیدا نشد");

            savedJob.JobId = request.JobId;
            savedJob.UserId = request.UserId;
            savedJob.IsActive = request.IsActive ?? savedJob.IsActive;

            await _unitOfWork.SavedJob.UpdateAsync(savedJob);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
