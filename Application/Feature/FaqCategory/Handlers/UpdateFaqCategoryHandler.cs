using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FaqCategory.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.FaqCategory.Handlers
{

    public class UpdateFaqCategoryHandler : IRequestHandler<UpdateFaqCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFaqCategoryHandler(IUnitOfWork context)
        {
            _unitOfWork = context;
        }

        public async Task<bool> Handle(UpdateFaqCategoryCommand request, CancellationToken cancellationToken)
        {

            var faqCategory = await _unitOfWork.FaqCategoriesRepository.GetByIdAsync(request.Id);
            if (faqCategory == null)
            {
                throw new NotFoundException("دسته بندی سوالات پیدا نشد");
            }

            faqCategory.Name = request.Name;
            faqCategory.DateModified = DateTime.Now;
            faqCategory.IsActive = request.IsActive;

            // Synchronize FaqQuestions
            var existingQuestions = faqCategory.Questions.ToList();
            var requestQuestionIds = request.faqQuestions.Where(q => q.Id != 0).Select(q => q.Id).ToList();

            // Remove questions not in the request
            foreach (var question in existingQuestions.ToList())
            {
                if (!requestQuestionIds.Contains(question.Id))
                {
                    faqCategory.Questions.Remove(question);
                }
            }

            // Add or update questions
            foreach (var q in request.faqQuestions)
            {
                var existingQuestion = faqCategory.Questions.FirstOrDefault(eq => eq.Id == q.Id && q.Id != 0);
                if (existingQuestion != null)
                {
                    // Update existing question
                    existingQuestion.Question = q.Question;
                    existingQuestion.Answer = q.Answer;
                    existingQuestion.DateModified = q.DateModified ?? DateTime.Now;
                    existingQuestion.IsActive = q.IsActive;
                    existingQuestion.FaqCategoryId = faqCategory.Id;
                }
                else
                {
                    // Add new question
                    var newQuestion = new JobFinder.Domain.Common.Entities.FaqQuestion
                    {
                        Question = q.Question,
                        Answer = q.Answer,
                        DateCreated = q.DateCreated ?? DateTime.Now,
                        DateModified = q.DateModified ?? DateTime.Now,
                        IsActive = q.IsActive,
                        FaqCategoryId = faqCategory.Id
                    };
                    faqCategory.Questions.Add(newQuestion);
                }
            }

            try
            {
                await _unitOfWork.FaqCategoriesRepository.UpdateAsync(faqCategory);
                foreach (var question in faqCategory.Questions)
                {
                    var dbQuestionrecord = await _unitOfWork.FaqQuestionsRepository.GetByIdAsync(question.Id);
                    if (dbQuestionrecord != null)
                    {
                        await _unitOfWork.FaqQuestionsRepository.UpdateRangeAsync(faqCategory.Questions);
                    }
                    else
                    {
                        await _unitOfWork.FaqQuestionsRepository.AddRangeAsync(faqCategory.Questions);
                    }
                }
                await _unitOfWork.CommitAsync(cancellationToken);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the exception for debugging
                // Example: _logger.LogError(ex, "Concurrency conflict while updating FAQ category {Id}", request.Id);
                throw new DbUpdateConcurrencyException("The FAQ category was modified or deleted by another user.", ex);
            }
        }
    }

}
