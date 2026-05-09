using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FaqCategory.Command;
using MediatR;

namespace JobFinder.Application.Feature.FaqCategory.Handlers
{
    public class DeleteFaqCategoryHandler : IRequestHandler<DeleteFaqCategoryCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteFaqCategoryHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFaqCategoryCommand request, CancellationToken cancellationToken)
        {
            var faqCategory = await _context.FaqCategoriesRepository.GetByIdAsync(request.Id);
            if (faqCategory == null)
            {

                throw new NotFoundException("دسته بندی سوالات پیدا نشد");
            }

            await _context.FaqCategoriesRepository.DeleteAsync(faqCategory);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
