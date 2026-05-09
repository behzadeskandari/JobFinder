using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.Cities.Command;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.Cities.Handlers
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var city = new Contracts.Dtos.DropDown.CityDto
            {
                Label = request.Dto.Label,
                ProvinceId = request.Dto.ProvinceId,
                IsActive = true
            };
            var cityRecord = new City
            {
                Label = request.Dto.Label,
                ProvinceId = request.Dto.ProvinceId,
                IsActive = true,
                Value = request.Dto.Value,
            };
            await _unitOfWork.CitiesRepository.AddAsync(cityRecord);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
