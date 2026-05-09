using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }
}
