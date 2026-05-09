using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FaqQuestion.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.FaqQuestion.Handlers
{
    public class UpdateFaqQuestionHandler : IRequestHandler<UpdateFaqQuestionCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public UpdateFaqQuestionHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateFaqQuestionCommand request, CancellationToken cancellationToken)
        {
            var faqQuestion = await _context.FaqQuestionsRepository.GetByIdAsync(request.Id);
            var faqCategory = await _context.FaqCategoriesRepository.GetByIdAsync(request.FaqcategoryId);
            if (faqCategory == null)
            {
                throw new NotFoundException("یک دسته بندی سوالات متداول موجود را وارد کنید یا یکی ایجاد کنید");
            }

            if (faqQuestion == null)
            {
                throw new NotFoundException("جواب سوالات پیدا نشد");
            }

            faqQuestion.Question = request.Question;
            faqQuestion.Answer = request.Answer;
            faqQuestion.DateModified = DateTime.Now;
            faqQuestion.IsActive = request.IsActive;

            await _context.FaqQuestionsRepository.UpdateAsync(faqQuestion);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
