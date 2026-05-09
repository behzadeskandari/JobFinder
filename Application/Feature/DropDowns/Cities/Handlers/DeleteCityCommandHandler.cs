using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.Cities.Command;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.Cities.Handlers
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.CitiesRepository.GetCityByIdAsync(request.Id);
            if (city == null)
            {
                throw new NotFoundException("شهر پیدا نشد");
            }
            await _unitOfWork.CitiesRepository.DeleteCityAsync(city.Id);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
