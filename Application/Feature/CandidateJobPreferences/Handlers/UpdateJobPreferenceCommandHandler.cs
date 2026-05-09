using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CandidateJobPreferences.Command;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.CandidateJobPreferences.Handlers
{
    public class UpdateJobPreferenceCommandHandler : IRequestHandler<UpdateJobPreferenceCommand, Result<CandidateJobPreferenceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateJobPreferenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CandidateJobPreferenceDto>> Handle(UpdateJobPreferenceCommand request, CancellationToken cancellationToken)
        {
            var preference = await _unitOfWork.candidateJobPreferences
                .GetByIdAsync(request.Id);

            if (preference == null || preference.IsActive != true)
                throw new NotFoundException("اولویت شغلی یافت نشد یا غیرفعال است");

            if (preference.UserId != request.UserId)
                throw new NotFoundException("دسترسی غیرمجاز به تنظیمات شغلی");

            if (request.JobCategoryId.HasValue)
            {
                var jobCategory = await _unitOfWork.candidateJobPreferences.GetByIdAsync(request.JobCategoryId.Value);
                if (jobCategory == null)
                    throw new NotFoundException("دسته شغلی یافت نشد");
            }

            if (request.CityId.HasValue)
            {
                var city = await _unitOfWork.CitiesRepository
                    .GetByIdAsync(request.CityId.Value);
                if (city == null)
                    throw new NotFoundException("شهر پیدا نشد");
            }

            preference.JobCategoryId = request.JobCategoryId;
            preference.PreferredCityId = request.CityId!.Value;
            preference.JobType = request.JobType;
            preference.MinSalary = request.MinSalary;

            await _unitOfWork.candidateJobPreferences.UpdateAsync(preference);
            await _unitOfWork.CommitAsync();

            var preferenceDto = _mapper.Map<CandidateJobPreferenceDto>(preference);
            return Result.Ok(preferenceDto);
        }
    }
}
