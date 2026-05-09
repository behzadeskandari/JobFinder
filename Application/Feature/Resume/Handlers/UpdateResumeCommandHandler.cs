using FluentResults;
using JobFinder.Application.Feature.Resume.Command;
using JobFinder.Application.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Common.Exceptions;

namespace JobFinder.Application.Feature.Resume.Handlers
{
    public class UpdateResumeCommandHandler : IRequestHandler<UpdateResumeCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateResumeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<Result> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != request.Resume.Id)
            {
                throw new NotFoundException("رزومه مورد نظر پیدا نشد");
            }

            request.Resume.UpdatedAt = DateTime.Now;

            try
            {
                var record = await _unitOfWork.ResumeRepository.UpdateResume(request.Id,request.Resume);
                if (record.IsPersisted)
                {
                    return new Result().WithSuccess("اطالاعات ذخیره شد");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.ResumeRepository.ResumeExists(request.Id))
                {
                    throw new NotFoundException("رزومه مورد نظر پیدا نشد");
                }
                else
                {
                    throw;
                }
            }

            return new Result().WithSuccess("Resume Updated SuccessFully");

        }
    }
}
