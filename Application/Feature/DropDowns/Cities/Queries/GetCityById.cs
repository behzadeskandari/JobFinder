using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.Cities.Queries
{
    public class GetCityById : IRequest<Result<List<CityDto>>>
    {
        public int ProvinceId { get; set; }
    }
}
