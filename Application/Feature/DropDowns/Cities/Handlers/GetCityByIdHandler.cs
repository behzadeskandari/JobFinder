using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.Cities.Queries;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.DropDowns.Cities.Handlers
{
    public class GetCityByIdHandler : IRequestHandler<GetCityById, Result<List<CityDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;

        public GetCityByIdHandler(IUnitOfWork unitOfWork//, 
            //IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            //_mapper = mapper;
        }

        public async Task<Result<List<CityDto>>> Handle(GetCityById request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.CitiesRepository.GetQueryable().Where(x => x.ProvinceId == request.ProvinceId).ToListAsync();
            if (city == null)
                throw new NotFoundException("شهر پیدا نشد");
            // var dto = _mapper.Map<CityDto>(city);
            var lst = city.Select(x => new CityDto
            {
                Id = x.Id,
                Label = x.Label,
                IsActive = x.IsActive.Value,
                ProvinceId = x.ProvinceId,
                Value = x.Value,
            }).ToList();
            return Result.Ok<List<CityDto>>(lst);
        }
    }
}
