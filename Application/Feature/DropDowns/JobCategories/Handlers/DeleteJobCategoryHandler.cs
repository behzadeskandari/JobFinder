using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.JobCategories.Command.DeleteJobCategoryCommand;
using JobFinder.Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Handlers
{
    public class DeleteJobCategoryHandler : IRequestHandler<DeleteJobCategoryCommand, bool>
    {
        private readonly IUnitOfWork _repository;

        public DeleteJobCategoryHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteJobCategoryCommand request, CancellationToken cancellationToken)
        {
            var jobCategory = await _repository.JobCategoryRepository.GetByIdAsync(request.Id);

            if (jobCategory == null)
                throw new NotFoundException("دسته بندی شغلی پیدا نشد");

            await _repository.JobCategoryRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);
            return true;
        }
    }
}
