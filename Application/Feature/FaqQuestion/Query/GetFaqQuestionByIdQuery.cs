using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.FaqQuestion.Query
{
    public record GetFaqQuestionByIdQuery(int Id) : IRequest<JobFinder.Domain.Common.Entities.FaqQuestion>;
}
