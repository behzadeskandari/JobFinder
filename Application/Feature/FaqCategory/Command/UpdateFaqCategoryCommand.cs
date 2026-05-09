using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.FaqCategory.Command
{

    public class UpdateFaqCategoryCommand : IRequest<bool> 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public List<JobFinder.Domain.Common.Entities.FaqQuestion> faqQuestions { get; set; }

    }
}
