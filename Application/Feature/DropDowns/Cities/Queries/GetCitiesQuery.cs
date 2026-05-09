using FluentResults;
using JobFinder.Contracts.Dtos.DropDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.Cities.Queries
{
    public class GetCitiesQuery : MediatR.IRequest<Result<IEnumerable<CityDto>>>
    {
        public int? ProvinceId { get; set; }
    }
}
