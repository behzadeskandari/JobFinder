using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PsychologyTests.Queries;
using JobFinder.Contracts.Dtos.PsychologyTest;
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
    public class GetPsychologyTestsQueryHandler : IRequestHandler<GetPsychologyTestsQuery, Result<IEnumerable<PsychologyTestDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPsychologyTestsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PsychologyTestDto>>> Handle(GetPsychologyTestsQuery request, CancellationToken cancellationToken)
        {
            var tests = await _unitOfWork.psychologyTest
                .GetQueryable()
                .Where(pt => pt.IsActive == true)
                .ToListAsync(cancellationToken);

            var testDtos = _mapper.Map<IEnumerable<PsychologyTestDto>>(tests);
            return Result.Ok(testDtos);
        }
    }
}
