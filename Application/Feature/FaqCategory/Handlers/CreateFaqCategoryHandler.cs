using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FaqCategory.Command;
using MediatR;

namespace JobFinder.Application.Feature.FaqCategory.Handlers
{

    public class CreateFaqCategoryHandler : IRequestHandler<CreateFaqCategoryCommand, JobFinder.Domain.Common.Entities.FaqCategory>
    {
        private readonly IUnitOfWork _context;

        public CreateFaqCategoryHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.FaqCategory> Handle(CreateFaqCategoryCommand request, CancellationToken cancellationToken)
        {
            var faqCategory = new JobFinder.Domain.Common.Entities.FaqCategory
            {
                Name = request.FaqCategory.Name,
                DateCreated = DateTime.Now,
                IsActive = true,
            };


            var record = await _context.FaqCategoriesRepository.AddAsync(faqCategory);
            foreach (var item in request.FaqCategory.Questions)
            {
                var faqQuestion = new JobFinder.Domain.Common.Entities.FaqQuestion
                {
                    Answer = item.Answer,
                    FaqCategory = record,
                    FaqCategoryId = record.Id,
                    Question = item.Question,
                    IsActive =true,
                };

               await _context.FaqQuestionsRepository.AddAsync(faqQuestion);
            }
            await _context.CommitAsync(cancellationToken);
            return faqCategory;
        }
    }
}
