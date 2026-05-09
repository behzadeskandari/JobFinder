using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Job.Command;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Job.Handlers
{
    public class UnsaveJobCommandHandler : IRequestHandler<UnsaveJobCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnsaveJobCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UnsaveJobCommand request, CancellationToken cancellationToken)
        {
            var savedJob = await _unitOfWork.SavedJob.FindAsync(sj => sj.JobId == request.JobId && sj.UserId == request.UserId && sj.IsActive == true);

            if (savedJob == null)
                throw new NotFoundException("شغل ذخیره شده یافت نشد");

            foreach (var item in savedJob)
            {
                item.IsActive = false;
            }
            await _unitOfWork.SavedJob.UpdateRangeAsync(savedJob);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
