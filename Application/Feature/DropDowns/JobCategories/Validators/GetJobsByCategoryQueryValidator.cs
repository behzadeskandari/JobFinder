using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JobFinder.Application.Feature.DropDowns.JobCategories.Queries;

namespace JobFinder.Application.Feature.DropDowns.JobCategories.Validators
{
    public class GetJobsByCategoryQueryValidator : AbstractValidator<GetJobsByCategoryQuery>
    {
        public GetJobsByCategoryQueryValidator()
        {
            RuleFor(x => x.Slug).NotEmpty().WithMessage("Category slug is required");
        }
    }
}
