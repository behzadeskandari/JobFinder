using FluentValidation;
using JobFinder.Application.Feature.Customers.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Customers.Validators
{
    public class GetCustomersQueryValidator : AbstractValidator<GetCustomersQuery>
    {
        public GetCustomersQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than zero");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
            RuleFor(x => x.CustomerType).Must(t => t == null || t == "Employer" || t == "JobSeeker").WithMessage("Customer type must be 'Employer' or 'JobSeeker'");
        }
    }
}
