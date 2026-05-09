using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PsychologyTests.Queries;
using JobFinder.Contracts.Dtos.PersonalityTest;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Handlers
{
    public class GetPersonalityTestsQueryHandler : IRequestHandler<GetPersonalityTestsQuery, Result<IEnumerable<PersonalityTestDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonalityTestsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PersonalityTestDto>>> Handle(GetPersonalityTestsQuery request, CancellationToken cancellationToken)
        {
            var tests = await _unitOfWork.psychologyTestResult.GetQueryable()
                .Where(pt => pt.IsActive == true)
                .ToListAsync(cancellationToken);

            var testDtos = _mapper.Map<IEnumerable<PersonalityTestDto>>(tests);
            return Result.Ok(testDtos);
        }
    }
}
