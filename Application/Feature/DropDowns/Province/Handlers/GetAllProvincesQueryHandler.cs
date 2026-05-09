using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.Province.Queries;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.Province.Handlers
{
    public class GetAllProvincesQueryHandler : IRequestHandler<GetAllProvincesQuery, Result<List<ProvinceDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProvincesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<ProvinceDto>>> Handle(GetAllProvincesQuery request, CancellationToken cancellationToken)
        {
            var provinces = await _unitOfWork.ProvincesRepository.GetAllProvincesAsync();
            var result =  provinces.Select(p => new ProvinceDto
            {
                Id = p.Id,
                Value = p.Id.ToString(),
                Label = p.Label,
                Cities = p.Cities.Select(c => new CityDto { Value = c.Value, Label = c.Label, Id = c.Id , ProvinceId = c.ProvinceId }).ToList()
                
            }).ToList();
            result = result.Where(x => x.Label != "").ToList();
            return Result.Ok(result);
        }
    }
}
