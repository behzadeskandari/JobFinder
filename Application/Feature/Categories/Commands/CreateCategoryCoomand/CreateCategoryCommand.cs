using JobFinder.Contracts.Dtos.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public CreateCategoryDto Category { get; set; }
    }

}
