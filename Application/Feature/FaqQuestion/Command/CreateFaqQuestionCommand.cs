using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.FaqQuestion.Command
{
    public record CreateFaqQuestionCommand(string Question, string Answer,int categoryId) : IRequest<int>;
}
