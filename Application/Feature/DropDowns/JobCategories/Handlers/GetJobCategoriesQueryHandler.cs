using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.JobCategories.Queries.GetJobCategoriesQuery;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Handlers
{
    public class GetJobCategoriesQueryHandler : IRequestHandler<GetJobCategoriesQuery, Result<List<JobCategoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetJobCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<JobCategoryDto>>> Handle(GetJobCategoriesQuery request, CancellationToken cancellationToken)
        {
            var jobCategories = await _unitOfWork.JobCategoryRepository.GetJobCategories();

            return Result.Ok(jobCategories.ToList());
        }
    }
}
