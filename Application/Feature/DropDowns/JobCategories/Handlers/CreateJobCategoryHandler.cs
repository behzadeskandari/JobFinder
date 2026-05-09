using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.JobCategories.Command.CreateJobCategoryCommand;
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
    public class CreateJobCategoryHandler : IRequestHandler<CreateJobCategoryCommand, JobCategory>
    {
        private readonly IUnitOfWork _repository;

        public CreateJobCategoryHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobCategory> Handle(CreateJobCategoryCommand request, CancellationToken cancellationToken)
        {
            var jobCategory = new JobCategory
            {
                Name = request.Name,
                NameEn = request.NameEn,
                Slug = request.Slug,
                Value = request.Value,
                IsActive = request.IsActive,
            };

            var r = await _repository.JobCategoryRepository.AddAsync(jobCategory);
            await _repository.CommitAsync(cancellationToken);
            return r; 
        }
    }
}
