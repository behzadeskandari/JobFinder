using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Candidate.Queries.GetCandidateResumesQuery;
using JobFinder.Contracts.Dtos.Resume;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Handlers.GetCandidateResumesQueryHandler
{
    public class GetCandidateResumesQueryHandler : IRequestHandler<GetCandidateResumesQuery, Result<IEnumerable<ResumeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCandidateResumesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ResumeDto>>> Handle(GetCandidateResumesQuery request, CancellationToken cancellationToken)
        {
            // Verify the candidate exists and is a JobSeeker
            var candidate = await _unitOfWork.CustomerRepository
                .GetByIdAsync(request.CandidateId);

            if (candidate == null || candidate.IsActive != true)
                throw new NotFoundException("کاندیدایی یافت نشد یا غیرفعال است");

            if (candidate.CustomerType != "JobSeeker")
                throw new NotFoundException("متقاضی جویای کار نیست");

            // Fetch resumes for the candidate
            var resumes = await _unitOfWork.CustomerRepository
                .GetQueryable()
                .Where(r => r.UserId == candidate.UserId && r.IsActive == true)
                .ToListAsync(cancellationToken);

            var resumeDtos = _mapper.Map<IEnumerable<ResumeDto>>(resumes);
            return Result.Ok(resumeDtos);
        }
    }
}
