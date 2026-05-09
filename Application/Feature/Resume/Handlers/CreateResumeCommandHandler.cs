using FluentResults;
using JobFinder.Application.Feature.Resume.Command;
using JobFinder.Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;

namespace JobFinder.Application.Feature.Resume.Handlers
{
    public class CreateResumeCommandHandler : IRequestHandler<CreateResumeCommand, Result<Domain.Common.Entities.Resume>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateResumeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Domain.Common.Entities.Resume>> Handle(CreateResumeCommand request, CancellationToken cancellationToken)
        {
            request.Resume.CreatedAt = DateTime.Now;
            request.Resume.UpdatedAt = DateTime.Now;

            var record = await _unitOfWork.ResumeRepository.CreateResume(request.Resume);
            if (record.IsPersisted)
            {
                
                return new Result<Domain.Common.Entities.Resume>()
                    .WithSuccess("Resume Created SuccessFully")
                    .WithValue(request.Resume);
            }
            else
            {
                return new Result<Domain.Common.Entities.Resume>()
                    .WithError("Error In Creation of the Resume Entity")
                    .WithValue(request.Resume);
            }
        }
    }
}
