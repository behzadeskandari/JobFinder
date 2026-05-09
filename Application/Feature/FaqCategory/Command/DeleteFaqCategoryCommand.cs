using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.FaqCategory.Command
{

    public record DeleteFaqCategoryCommand(int Id) : IRequest<bool>;
}
