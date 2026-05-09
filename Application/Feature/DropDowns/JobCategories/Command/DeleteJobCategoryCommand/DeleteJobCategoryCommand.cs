using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Command.DeleteJobCategoryCommand
{
    public class DeleteJobCategoryCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
