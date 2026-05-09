using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Command.CreateJobCategoryCommand
{
    public class CreateJobCategoryCommand : IRequest<JobCategory>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Slug { get; set; }
        public string Value { get; set; } 
        public bool? IsActive { get; set; }
    }
}
