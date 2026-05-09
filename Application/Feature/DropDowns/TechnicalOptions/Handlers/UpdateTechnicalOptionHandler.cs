using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.TechnicalOptions.Command;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.TechnicalOptions.Handlers
{
    public class UpdateTechnicalOptionHandler : IRequestHandler<UpdateTechnicalOptionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTechnicalOptionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTechnicalOptionCommand request, CancellationToken cancellationToken)
        {
            var option = await _unitOfWork.TechnicalOptionsRepository.GetByIdAsyncTechnical(request.Dto.Id);
            if (option == null)
                throw new NotFoundException("گزینه یافت نشد");

            option.Label = request.Dto.Label;
            option.Value = request.Dto.Value;
            option.IsActive = request.Dto.IsActive;

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
