using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.TechnicalOptions.Queries;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;

namespace JobFinder.Application.Feature.DropDowns.TechnicalOptions.Handlers
{
    public class GetTechnicalOptionByIdHandler : IRequestHandler<GetTechnicalOptionByIdQuery, Result<TechnicalOptionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTechnicalOptionByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<TechnicalOptionDto>> Handle(GetTechnicalOptionByIdQuery request, CancellationToken cancellationToken)
        {
            var option = await _unitOfWork.TechnicalOptionsRepository.GetByIdAsyncTechnical(request.Id);
            if (option == null)
                throw new NotFoundException("گزینه یافت نشد");

            var dto = _mapper.Map<TechnicalOptionDto>(option);
            return Result.Ok(dto);
        }
    }
}
