using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.DropDowns.JobCategories.Queries.GetJobCategoriesQuery;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Validators
{
    public class GetJobCategoriesQueryValidator : AbstractValidator<GetJobCategoriesQuery>
    {
        public GetJobCategoriesQueryValidator()
        {
            // No validation needed
        }
    }
}
