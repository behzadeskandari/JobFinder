using FluentValidation;
using JobFinder.Application.Feature.Customers.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Customers.Validators
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Customer ID is required");
        }
    }
}
