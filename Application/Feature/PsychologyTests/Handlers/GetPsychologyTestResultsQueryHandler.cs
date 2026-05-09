using AutoMapper;
using FluentResults;
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
    public class GetPsychologyTestResultsQueryHandler : IRequestHandler<GetPsychologyTestResultsQuery, Result<IEnumerable<PsychologyTestResultDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPsychologyTestResultsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PsychologyTestResultDto>>> Handle(GetPsychologyTestResultsQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.psychologyTestResult
                .GetQueryable()
                .Include(ptr => ptr.PsychologyTest)
                .Where(ptr => ptr.UserId == request.UserId && ptr.IsActive == true)
                .ToListAsync(cancellationToken);

            var resultDtos = _mapper.Map<IEnumerable<PsychologyTestResultDto>>(results);
            return Result.Ok(resultDtos);
        }
    }
}
