using FluentResults;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.TechnicalOptions.Queries
{
    public class GetTechnicalOptionsQuery : IRequest<Result<IEnumerable<TechnicalOptionDto>>>
    {
    }
}
