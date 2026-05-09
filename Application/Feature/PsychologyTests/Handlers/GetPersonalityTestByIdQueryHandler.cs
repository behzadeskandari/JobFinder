using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PsychologyTests.Queries;
using JobFinder.Contracts.Dtos.PersonalityTest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Handlers
{
    public class GetPersonalityTestByIdQueryHandler : IRequestHandler<GetPersonalityTestByIdQuery, Result<PersonalityTestDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonalityTestByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PersonalityTestDto>> Handle(GetPersonalityTestByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.personalityTrait
                .GetByIdAsync(request.Id);

            if (test == null || test.IsActive != true)
                throw new NotFoundException("تست شخصیت پیدا نشد و یا غیر فعال است");

            var testDto = _mapper.Map<PersonalityTestDto>(test);
            return Result.Ok(testDto);
        }
    }
}
