using System;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Resume.Command;
using MediatR;

namespace JobFinder.Application.Feature.Resume.Handlers
{
    public class DeleteResumeCommandHandler : IRequestHandler<DeleteResumeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteResumeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resume = await _unitOfWork.ResumeRepository.GetByIdAsync(request.Id);
                if (resume == null)
                {
                    throw new NotFoundException("????? ???? ??? ???? ???");
                }

                await _unitOfWork.ResumeRepository.DeleteAsync(resume);
                await _unitOfWork.CommitAsync();
                
                return true;
            }
            catch (Exception)
            {
                // Log the exception here if needed

                throw new NotFoundException("????? ???? ??? ???? ???");
            }
        }
    }
}
