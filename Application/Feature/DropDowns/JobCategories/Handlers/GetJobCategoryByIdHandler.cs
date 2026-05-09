using JobFinder.Application.Feature.DropDowns.JobCategories.Queries.GetJobCategoryByIdQuery;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Handlers
{
    public class GetJobCategoryByIdHandler : IRequestHandler<GetJobCategoryByIdQuery, JobCategory>
    {
        private readonly IJobCategoryRepository _repository;

        public GetJobCategoryByIdHandler(IJobCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<JobCategory> Handle(GetJobCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
