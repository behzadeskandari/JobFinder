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
    public class CreateFaqQuestionHandler : IRequestHandler<CreateFaqQuestionCommand, int>
    {
        private readonly IUnitOfWork _context;

        public CreateFaqQuestionHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFaqQuestionCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.FaqCategoriesRepository.GetByIdAsync(request.categoryId);
            if(category == null)
            {
                throw new NotFoundException("ابتدا باید دسته‌بندی سوالات متداول را وارد کنید، سپس سوالات را وارد کنید");
            }
            var faqQuestion = new JobFinder.Domain.Common.Entities.FaqQuestion
            {
                Question = request.Question,
                Answer = request.Answer,
                DateCreated = DateTime.Now,
                IsActive = true,
                FaqCategory = category,
                FaqCategoryId = category.Id
            };

            await _context.FaqQuestionsRepository.AddAsync(faqQuestion);
            await _context.CommitAsync(cancellationToken);
            return faqQuestion.Id;
        }
    }
}
