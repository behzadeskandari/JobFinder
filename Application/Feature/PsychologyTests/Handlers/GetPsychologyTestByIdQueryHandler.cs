using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PsychologyTests.Queries;
using JobFinder.Contracts.Dtos.PsychologyTest;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Handlers
{
    public class GetPsychologyTestByIdQueryHandler : IRequestHandler<GetPsychologyTestByIdQuery, Result<PsychologyTestDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPsychologyTestByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PsychologyTestDto>> Handle(GetPsychologyTestByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.psychologyTest
                .GetByIdAsync(request.Id);

            if (test == null || test.IsActive != true)
                throw new NotFoundException("تست شخصیت پیدا نشد و یا غیر فعال است");

            var testDto = _mapper.Map<PsychologyTestDto>(test);
            return Result.Ok(testDto);
        }
    }
}
