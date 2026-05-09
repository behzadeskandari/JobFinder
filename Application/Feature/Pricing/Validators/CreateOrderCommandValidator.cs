using FluentValidation;
using JobFinder.Application.Feature.Pricing.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Pricing.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
            //RuleFor(x => x.PlanId).GreaterThan(0).WithMessage("Pricing plan ID is required");
            //RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero");
        }
    }
}
