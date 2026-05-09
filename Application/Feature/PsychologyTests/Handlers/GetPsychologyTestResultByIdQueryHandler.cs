using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PsychologyTests.Queries;
using JobFinder.Contracts.Dtos.PsychologyTestResult;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Handlers
{
    public class GetPsychologyTestResultByIdQueryHandler : IRequestHandler<GetPsychologyTestResultByIdQuery, Result<PsychologyTestResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPsychologyTestResultByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PsychologyTestResultDto>> Handle(GetPsychologyTestResultByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.psychologyTestResult.GetQueryable()
                .Include(ptr => ptr.PsychologyTest)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result == null || result.IsActive != true)
                throw new NotFoundException("تست شخصیت پیدا نشد و یا غیر فعال است");

            if (result.UserId != request.UserId)
                throw new UnauthorizedAccessException("دسترسی غیرمجاز به نتایج آزمایش");

            var resultDto = _mapper.Map<PsychologyTestResultDto>(result);
            return Result.Ok(resultDto);
        }
    }
}
