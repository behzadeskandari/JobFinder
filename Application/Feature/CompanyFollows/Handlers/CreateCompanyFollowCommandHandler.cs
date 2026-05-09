using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CompanyFollows.Command;
using JobFinder.Contracts.Dtos.CompanyFollows;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.CompanyFollows.Handlers
{
    public class CreateCompanyFollowCommandHandler : IRequestHandler<CreateCompanyFollowCommand, Result<CompanyFollowDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCompanyFollowCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CompanyFollowDto>> Handle(CreateCompanyFollowCommand request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null || company.IsActive != true)
                throw new NotFoundException("شرکت یافت نشد یا غیرفعال است");

            var user = await _unitOfWork.companyRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException("کاربر پیدا نشد");

            var companyFollow = new CompanyFollow
            {
                CompanyId = request.CompanyId,
                UserId = request.UserId,
                DateCreated = DateTime.Now,
                IsActive = true
            };
            await _unitOfWork.CompanyFollowRepository.AddAsync(companyFollow);
            await _unitOfWork.CommitAsync(cancellationToken);

            var companyFollowDto = _mapper.Map<CompanyFollowDto>(companyFollow);
            return Result.Ok(companyFollowDto);
        }
    }
}
