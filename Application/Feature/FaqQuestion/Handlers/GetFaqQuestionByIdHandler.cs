using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.FaqQuestion.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.FaqQuestion.Handlers
{
    public class GetFaqQuestionByIdHandler : IRequestHandler<GetFaqQuestionByIdQuery, JobFinder.Domain.Common.Entities.FaqQuestion>
    {
        private readonly IUnitOfWork _context;

        public GetFaqQuestionByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.FaqQuestion> Handle(GetFaqQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _context.FaqQuestionsRepository.GetByIdAsync(request.Id);
            return record;
        }
    }
}
