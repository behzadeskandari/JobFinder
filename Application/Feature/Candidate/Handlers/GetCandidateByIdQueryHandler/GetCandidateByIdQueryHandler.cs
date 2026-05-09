using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Candidate.Queries.GetCandidateByIdQuery;
using JobFinder.Contracts.Dtos.Candidate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Handlers.GetCandidateByIdQueryHandler
{
    public class GetCandidateByIdQueryHandler : IRequestHandler<GetCandidateByIdQuery, Result<CandidateGetDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCandidateByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CandidateGetDto>> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CandidateRepository.GetCandidateDto(request.Id);

            if (result.IsSuccess)
            {
                Result.Ok(result.Value);
            }
            
            throw new NotFoundException("شکست در دریافت کاندید ");        
        }
    }
}
