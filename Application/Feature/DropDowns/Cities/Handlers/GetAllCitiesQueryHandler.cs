using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.Cities.Queries;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.Cities.Handlers
{
    internal class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, Result<List<CityDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCitiesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<CityDto>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _unitOfWork.CitiesRepository.GetAllCitiesAsync();
            return Result.Ok(cities);
        }
    }
}
