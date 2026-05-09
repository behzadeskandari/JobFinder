using FluentValidation;
using JobFinder.Application.Feature.Customers.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Customers.Validators
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Customer ID is required");
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100).WithMessage("FirstName is required and must not exceed 100 characters");
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100).WithMessage("LastName is required and must not exceed 100 characters");
            RuleFor(x => x.PostalCode).NotEmpty().EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.City).MaximumLength(20).WithMessage("Phone number must not exceed 20 characters");
            RuleFor(x => x.CustomerType).NotEmpty().Must(t => t == "Staff" || t == "User").WithMessage("Customer type must be 'Employer' or 'JobSeeker'");
        }
    }
}
