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

namespace JobFinder.Application.Feature.SavedJobs.Handlers
{
    public class DeleteSavedJobCommandHandler : IRequestHandler<DeleteSavedJobCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSavedJobCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteSavedJobCommand request, CancellationToken cancellationToken)
        {
            JobFinder.Domain.Common.Entities.Job savedJob = await _unitOfWork.JobsRepository.GetByIdAsync(request.Id);

            if (savedJob == null)
                throw new NotFoundException("شغل ذخیره شده پیدا نشد");

            savedJob.IsActive = false; // Soft delete

            
            var savedJobs = await _unitOfWork.SavedJob.FindAsync(x=> x.Id == savedJob.Id);
            await _unitOfWork.SavedJob.UpdateRangeAsync(savedJobs);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
