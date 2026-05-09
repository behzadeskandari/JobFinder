using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.JobCategories.Queries;
using JobFinder.Contracts.Dtos.Job;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Handlers
{
    public class GetJobsByCategoryQueryHandler : IRequestHandler<GetJobsByCategoryQuery, Result<IEnumerable<JobGetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetJobsByCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<JobGetDto>>> Handle(GetJobsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _unitOfWork.JobsRepository
                .GetQueryable()
                .Include(j => j.Company)
                .Include(j => j.Cities)
                .Include(j => j.JobCategories)
                .Where(j => j.JobCategories.Slug == request.Slug && j.IsActive == true)
                .ToListAsync(cancellationToken);

            var jobDtos = _mapper.Map<IEnumerable<JobGetDto>>(jobs);
            return Result.Ok(jobDtos);
        }
    }
}
