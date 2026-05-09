using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.PsychologyTests.Command;
using JobFinder.Contracts.Dtos.PersonalityTestResult;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.PsychologyTests.Handlers
{
    public class CreatePersonalityTestResultCommandHandler : IRequestHandler<CreatePersonalityTestResultCommand, Result<PersonalityTestResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePersonalityTestResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PersonalityTestResultDto>> Handle(CreatePersonalityTestResultCommand request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.personalityTrait.GetByIdAsync(request.PersonalityTestId);
            if (test == null || test.IsActive != true)
                throw new NotFoundException("تست شخصیت شناسی پیدا نشد");

            var user = await _unitOfWork.personalityTrait.GetByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException("کاربر پیدا نشد");

            var result = new PersonalityTestResult
            {
                //PersonalityTestId = request.PersonalityTestId,
                UserId = request.UserId,
                //ResultData = request.ResultData,
                //DateTaken = DateTime.Now,
                IsActive = true
            };
            //:TODO
            //await _unitOfWork.WriteRepository<PersonalityTestResult>().AddAsync(result);
            //await _unitOfWork.CompleteAsync();

            var resultDto = _mapper.Map<PersonalityTestResultDto>(result);
            return Result.Ok(resultDto);
        }
    }
}
