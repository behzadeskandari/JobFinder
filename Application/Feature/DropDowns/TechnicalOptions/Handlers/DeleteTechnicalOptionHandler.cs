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
    public class DeleteTechnicalOptionHandler : IRequestHandler<DeleteTechnicalOptionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTechnicalOptionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTechnicalOptionCommand request, CancellationToken cancellationToken)
        {
            var option = await _unitOfWork.TechnicalOptionsRepository.GetByIdAsyncTechnical(request.Id);
            if (option == null)
                throw new NotFoundException("گزینه یافت نشد");

            _unitOfWork.TechnicalOptionsRepository.DeleteTechnical(option);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
