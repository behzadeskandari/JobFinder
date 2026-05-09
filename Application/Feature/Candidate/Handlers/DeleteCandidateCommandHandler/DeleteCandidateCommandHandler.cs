using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Candidate.Command.DeleteCandidateCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Candidate.Handlers.DeleteCandidateCommandHandler
{
    public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCandidateCommandHandler(IUnitOfWork _unitOfWork)
        {
            //_context = context;
            _unitOfWork = _unitOfWork ?? throw new ArgumentNullException("Enttity Null Exception");
        }

        public async Task<Result<string>> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {


           var result =  await _unitOfWork.CandidateRepository.CandidateFindAsync(request.Id);


           var modifiedResult =  await _unitOfWork.CandidateRepository.RemoveCandidate(result.Value);

            if (modifiedResult.IsSuccess)
            {
                return Result.Ok(modifiedResult.Value);
            }

            throw new NotFoundException("حذف نامزد مشکل دارد");
        }
    }
}
