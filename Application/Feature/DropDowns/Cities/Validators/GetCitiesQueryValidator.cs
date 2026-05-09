using FluentValidation;
using JobFinder.Application.Feature.DropDowns.Cities.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.Cities.Validators
{
    public class GetCitiesQueryValidator : AbstractValidator<GetCitiesQuery>
    {
        public GetCitiesQueryValidator()
        {
            RuleFor(x => x.ProvinceId).GreaterThan(0).When(x => x.ProvinceId.HasValue).WithMessage("Invalid province ID");
        }
    }
}
