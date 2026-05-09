using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.JobCategories.Command.UpdateJobCategoryCommand;
using JobFinder.Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Handlers
{
    public class UpdateJobCategoryHandler : IRequestHandler<UpdateJobCategoryCommand, bool>
    {
        private readonly IUnitOfWork _repository;

        public UpdateJobCategoryHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateJobCategoryCommand request, CancellationToken cancellationToken)
        {
            var jobCategory = await _repository.JobCategoryRepository.GetByIdAsync(request.Id);

            if (jobCategory == null)
                return false;

            jobCategory.Name = request.Name;
            jobCategory.NameEn = request.NameEn;
            jobCategory.Slug = request.Slug;

            await _repository.JobCategoryRepository.UpdateAsync(jobCategory);
            await _repository.CommitAsync(cancellationToken);
            return true;
        }
    }
}
