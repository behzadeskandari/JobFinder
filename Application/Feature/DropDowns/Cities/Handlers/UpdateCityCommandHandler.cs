using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.Cities.Command;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.Cities.Handlers
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.CitiesRepository.GetCityByIdAsync(request.Dto.Id);
            if (city == null)
                return Result.Fail("City not found");

            var province = await _unitOfWork.ProvincesRepository.GetProvinceWithCityByIdAsync(request.Dto.ProvinceId);
            if (province != null)
            {
                await _unitOfWork.CitiesRepository.UpdateCityAsync(city, request.Dto.ProvinceId);
            }
            else
            {
                city.Label = request.Dto.Label;
                city.ProvinceId = request.Dto.ProvinceId;
                city.IsActive = request.Dto.IsActive.HasValue ? request.Dto.IsActive.Value : false;
                await _unitOfWork.CitiesRepository.UpdateCityAsync(city);
            }
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
