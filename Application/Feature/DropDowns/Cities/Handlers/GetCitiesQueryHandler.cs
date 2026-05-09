using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.Cities.Queries;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.Cities.Handlers
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, Result<IEnumerable<CityDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CityDto>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.CitiesRepository
                .GetQueryable()
                //.Include(c => c.Province)
                .Where(c => c.IsActive == true);

            if (request.ProvinceId.HasValue)
            {
                var province = _unitOfWork.ProvincesRepository.GetQueryable().Where(c => c.IsActive == true && c.Id == request.ProvinceId.Value).ToList();
                foreach (var item in query)
                {
                    foreach (var p in province)
                    {
                        if (p.Id == item.ProvinceId)
                        {
                            //item.Province = p;
                            item.ProvinceId = p.Id;
                        }
                    }
                }
            }
            //query = query.Where(c => c.ProvinceId == request.ProvinceId.Value);
            

            var cities = await query.ToListAsync(cancellationToken);
            var cityDtos = _mapper.Map<IEnumerable<CityDto>>(cities);

            return Result.Ok(cityDtos);
        }
    }
}
