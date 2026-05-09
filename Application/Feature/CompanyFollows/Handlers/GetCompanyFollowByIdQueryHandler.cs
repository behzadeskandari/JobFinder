using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CompanyFollows.Queries;
using JobFinder.Contracts.Dtos.CompanyFollows;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.CompanyFollows.Handlers
{
    public class GetCompanyFollowByIdQueryHandler : IRequestHandler<GetCompanyFollowByIdQuery, Result<CompanyFollowDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCompanyFollowByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CompanyFollowDto>> Handle(GetCompanyFollowByIdQuery request, CancellationToken cancellationToken)
        {
            var companyFollow = await _unitOfWork.CompanyFollowRepository
                .GetQueryable().Where(x => x.Id == request.Id)
                .Include(cf => cf.Company)
                .FirstOrDefaultAsync(cancellationToken);

            if (companyFollow == null || companyFollow.IsActive != true)
                throw new NotFoundException("دنبال کننده شرکت یافت نشد یا غیرفعال است");

            var companyFollowDto = _mapper.Map<CompanyFollowDto>(companyFollow);
            return Result.Ok(companyFollowDto);//
        }
    }
}
