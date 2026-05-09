using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.TechnicalOptions.Command;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.TechnicalOptions.Handlers
{
    public class CreateTechnicalOptionHandler : IRequestHandler<CreateTechnicalOptionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTechnicalOptionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateTechnicalOptionCommand request, CancellationToken cancellationToken)
        {
            var option = new TechnicalOption
            {
                Label = request.Dto.Label,
                Value = request.Dto.Value,
                IsActive = true
            };

            await _unitOfWork.TechnicalOptionsRepository.AddAsyncTechnical(option);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
