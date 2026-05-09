using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.Cities;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.Cities.Command
{
    public record UpdateCityCommand(UpdateCityDto Dto) : IRequest<Result>;
}
