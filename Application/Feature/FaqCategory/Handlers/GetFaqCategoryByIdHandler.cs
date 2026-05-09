using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FaqCategory.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.FaqCategory.Handlers
{
    public class GetFaqCategoryByIdHandler : IRequestHandler<GetFaqCategoryByIdQuery,JobFinder.Domain.Common.Entities.FaqCategory>
    {
        private readonly IUnitOfWork _context;

        public GetFaqCategoryByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.FaqCategory> Handle(GetFaqCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.FaqCategoriesRepository.GetQueryable()
                .Include(c => c.Questions) // Include the related questions
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        }
    }
}
