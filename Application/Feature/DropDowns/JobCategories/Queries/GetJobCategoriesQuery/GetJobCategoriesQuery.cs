using FluentResults;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Queries.GetJobCategoriesQuery
{
    public class GetJobCategoriesQuery : IRequest<Result<List<JobCategoryDto>>>
    {
        // Optional: Add any parameters needed for the query
    }
}
