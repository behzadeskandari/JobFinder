using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Queries.GetJobCategoryByIdQuery
{
    public class GetJobCategoryByIdQuery : IRequest<JobCategory>
    {
        public int Id { get; set; }
    }
}
