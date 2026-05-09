using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CandidateJobPreferences.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.CandidateJobPreferences.Handlers
{
    public class DeleteJobPreferenceCommandHandler : IRequestHandler<DeleteJobPreferenceCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteJobPreferenceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteJobPreferenceCommand request, CancellationToken cancellationToken)
        {
            var preference = await _unitOfWork.candidateJobPreferences
                .GetByIdAsync(request.Id);

            if (preference == null || preference.IsActive != true)
                throw new NotFoundException("اولویت شغلی یافت نشد یا غیرفعال است");

            if (preference.UserId != request.UserId)
                throw new NotFoundException("دسترسی غیرمجاز به تنظیمات شغلی");

            preference.IsActive = false; // Soft delete
            await _unitOfWork.candidateJobPreferences.UpdateAsync(preference);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
