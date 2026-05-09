using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Candidate.Queries.GetCandidatesQuery;
using JobFinder.Contracts.Dtos.Candidate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Handlers.GetCandidatesQueryHandler
{
    public class GetCandidatesQueryHandler : IRequestHandler<GetCandidatesQuery, Result<IEnumerable<CandidateGetDto>>>
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCandidatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CandidateGetDto>>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CandidateRepository.GetCandidate();

            if (result.IsSuccess)
            {
                var candidateDtos = _mapper.Map<Result<IEnumerable<CandidateGetDto>>>(result);

                return candidateDtos;
            }
            throw new NotFoundException("دریافت کاندید ناموفق بود");
        }
    }
}
