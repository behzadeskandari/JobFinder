using FluentResults;
using JobFinder.Contracts.Dtos.Province;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.Province.Command
{
    public record UpdateProvinceCommand(UpdateProvinceDto Dto) : IRequest<Result>;

}
