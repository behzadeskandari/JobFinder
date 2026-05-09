using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.FaqQuestion.Query
{

    public record GetAllFaqQuestionsQuery : IRequest<List<JobFinder.Domain.Common.Entities.FaqQuestion>>;
}
