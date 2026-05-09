using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.Province.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.Province.Handlers
{
    public class UpdateProvinceCommandHandler : IRequestHandler<UpdateProvinceCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProvinceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
        {
            var province = await _unitOfWork.ProvincesRepository.GetProvinceById(request.Dto.Id);
            if (province == null)
                throw new NotFoundException("استان پیدا نشد");

            province.Label = request.Dto.Label;
            province.IsActive = request.Dto.IsActive.Value;

            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
