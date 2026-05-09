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
    public class CreateJobPreferenceCommandHandler : IRequestHandler<CreateJobPreferenceCommand, Result<CandidateJobPreferenceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateJobPreferenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CandidateJobPreferenceDto>> Handle(CreateJobPreferenceCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.candidateJobPreferences.GetByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException("کاربر پیدا نشد");

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

            var preference = new JobFinder.Domain.Common.Entities.CandidateJobPreferences
            {
                UserId = request.UserId,
                JobCategoryId = request.JobCategoryId,
                PreferredCityId = request.CityId!.Value,
                JobType = request.JobType,
                MinSalary = request.MinSalary,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _unitOfWork.candidateJobPreferences.AddAsync(preference);
            await _unitOfWork.CommitAsync();

            var preferenceDto = _mapper.Map<CandidateJobPreferenceDto>(preference);
            return Result.Ok(preferenceDto);
        }
    }
}
