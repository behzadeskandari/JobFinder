using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.TechnicalOptions.Queries
{
    public class GetTechnicalOptionByIdQuery : IRequest<Result<TechnicalOptionDto>>
    {
        public int Id { get; set; }
    }
}
