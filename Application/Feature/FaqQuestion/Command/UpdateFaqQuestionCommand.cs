using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.FaqQuestion.Command
{
    public record UpdateFaqQuestionCommand(int Id, string Question, string Answer, bool? IsActive,int FaqcategoryId) : IRequest<bool>;
}
