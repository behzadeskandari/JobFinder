using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FaqQuestion.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.FaqQuestion.Handlers
{
    public class DeleteFaqQuestionHandler : IRequestHandler<DeleteFaqQuestionCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteFaqQuestionHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFaqQuestionCommand request, CancellationToken cancellationToken)
        {
            var faqQuestion = await _context.FaqQuestionsRepository.GetByIdAsync(request.Id);
            if (faqQuestion == null)
            {
                throw new NotFoundException("دسته بندی سوالات پیدا نشد");
            }

            await _context.FaqQuestionsRepository.DeleteAsync(faqQuestion);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }

}
