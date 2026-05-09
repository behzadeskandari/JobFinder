using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Candidate.Command.UpdateCandidateCommand;
using JobFinder.Contracts.Dtos.Candidate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Handlers.UpdateCandidateCommandHandler
{
    public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCandidateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {

            CandidateDto candidate = new CandidateDto();
            candidate.Phone = request.CandidateDto.Phone;
            candidate.FirstName = request.CandidateDto.FirstName;
            candidate.LastName = request.CandidateDto.LastName;
            candidate.Email = request.CandidateDto.Email;
            candidate.CoverLetter = request.CandidateDto.CoverLetter;

            var result = await _unitOfWork.CandidateRepository.UpdateCandidateAsync(request.Id, candidate);

            if (result.IsSuccess)
            {
                return Result.Ok(result.Value);
            }
            throw new NotFoundException("خطا در پیدا کردن تست MTBI");
        }
    }
}
