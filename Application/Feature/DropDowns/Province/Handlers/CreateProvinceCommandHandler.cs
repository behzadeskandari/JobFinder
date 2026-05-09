using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.DropDowns.Province.Command;
using JobFinder.Contracts.Dtos.DropDown;

namespace JobFinder.Application.Feature.DropDowns.Province.Handlers
{
    public class CreateProvinceCommandHandler : IRequestHandler<CreateProvinceCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProvinceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateProvinceCommand request, CancellationToken cancellationToken)
        {
            var province = new ProvinceDto
            {
                Label = request.Dto.Label,
                IsActive = true,
                Value = request.Dto.Value
            };

            await _unitOfWork.ProvincesRepository.AddProvinceAsync(province);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
