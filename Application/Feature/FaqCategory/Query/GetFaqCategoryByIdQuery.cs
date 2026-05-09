using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.FaqCategory.Query
{
    public record GetFaqCategoryByIdQuery(int Id) : IRequest<JobFinder.Domain.Common.Entities.FaqCategory>;

}
