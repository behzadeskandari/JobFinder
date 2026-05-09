using FluentValidation;
using JobFinder.Application.Feature.Blogs.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Blogs.Validators
{
    public class GetBlogByIdQueryValidator : AbstractValidator<GetBlogByIdQuery>
    {
        public GetBlogByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Blog ID is required");
        }
    }
}
