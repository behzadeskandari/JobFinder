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
    public class GetAllFaqQuestionsHandler : IRequestHandler<GetAllFaqQuestionsQuery, List< JobFinder.Domain.Common.Entities.FaqQuestion>>
    {
        private readonly IUnitOfWork _context;

        public GetAllFaqQuestionsHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.FaqQuestion>> Handle(GetAllFaqQuestionsQuery request, CancellationToken cancellationToken)
        {
            var record  = await _context.FaqQuestionsRepository.GetAllAsync(cancellationToken);

            return record.ToList();
        }
    }

}
