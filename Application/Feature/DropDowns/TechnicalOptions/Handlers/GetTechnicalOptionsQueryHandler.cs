using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.TechnicalOptions.Queries;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.TechnicalOptions.Handlers
{
    public class GetTechnicalOptionsQueryHandler : IRequestHandler<GetTechnicalOptionsQuery, Result<IEnumerable<TechnicalOptionDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTechnicalOptionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<TechnicalOptionDto>>> Handle(GetTechnicalOptionsQuery request, CancellationToken cancellationToken)
        {
            var technicalOptions = await _unitOfWork.TechnicalOptionsRepository.GetTechnicalOptionsTechnical();
            return Result.Ok(technicalOptions);
        }
    }
}
