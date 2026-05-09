using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.TechnicalOptions.Command
{
    public record DeleteTechnicalOptionCommand(int Id) : IRequest<Result>;
}
