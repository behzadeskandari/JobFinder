using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.Cities.Command
{
    public record DeleteCityCommand(int Id) : IRequest<Result>; 
}
