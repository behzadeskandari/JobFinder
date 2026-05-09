using JobFinder.Contracts.Dtos.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Categories.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateCategoryDto Category { get; set; }
    }
}
