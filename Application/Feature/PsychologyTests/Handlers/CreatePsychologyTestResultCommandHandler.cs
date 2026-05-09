using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PsychologyTests.Command;
using JobFinder.Contracts.Dtos.PsychologyTestResult;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Handlers
{
    public class CreatePsychologyTestResultCommandHandler : IRequestHandler<CreatePsychologyTestResultCommand, Result<PsychologyTestResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePsychologyTestResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PsychologyTestResultDto>> Handle(CreatePsychologyTestResultCommand request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.psychologyTest
                .GetByIdAsync(request.PsychologyTestId);
            if (test == null || test.IsActive != true)
                throw new NotFoundException("تست روانشناسی پیدا نشد");

            var user = await _unitOfWork.psychologyTest.GetByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException("کاربر پیدا نشد");

            var result = new PsychologyTestResult
            {
                PsychologyTestId = request.PsychologyTestId,
                UserId = request.UserId,
                TotalScore = request.TotalScore,
                ResultData = request.ResultData,
                SubmissionDate = DateTime.Now,
                DateTaken = DateTime.Now,
                IsActive = true
            };
            result.Interpretation.Add(request.Interpretation);
            
            await _unitOfWork.psychologyTestResult.AddAsync(result);
            await _unitOfWork.CommitAsync(cancellationToken);

            var resultDto = _mapper.Map<PsychologyTestResultDto>(result);
            return Result.Ok(resultDto);
        }
    }

}
