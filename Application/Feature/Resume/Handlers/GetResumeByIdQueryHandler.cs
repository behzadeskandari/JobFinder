using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Resume.Queries;
using JobFinder.Contracts.Dtos.Resume;
using MediaBrowser.Model.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace JobFinder.Application.Feature.Resume.Handlers
{
    internal class GetResumeByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetResumeByIdQuery,Result<ResumeDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<ResumeDto>> Handle(GetResumeByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.ResumeRepository.GetResume(request.Id);

            if (record == null)
            {
                return new Result<ResumeDto>().WithError("Resume Record NotFound404"); 
            }
            else
            {
                var resumeDto = CreateResumeDto(record);
                return new Result<ResumeDto>().WithValue(resumeDto);
            }

        }

        private static ResumeDto CreateResumeDto(Domain.Common.Entities.Resume record)
        {
            var resumeDto = new ResumeDto
            {
                Address = record.Address,
                Educations = record.Educations.Select(edu => new EducationDto
                {
                    Degree = edu.Degree,
                    Description = edu.Description,
                    EndDate = edu.EndDate,
                    Field = edu.Field,
                    Institution = edu.Institution,
                    ResumeId = edu.ResumeId,
                    StartDate = edu.StartDate,
                }).ToList(),
                WorkExperiences = record.WorkExperiences.Select(work => new WorkExperienceDto
                {
                    Description = work.Description,
                    EndDate = work.EndDate,
                    ResumeId = work.ResumeId,
                    StartDate = work.StartDate,
                    CompanyName = work.CompanyName,
                    IsCurrentJob = work.IsCurrentJob,
                    JobTitle = work.JobTitle,
                }).ToList(),
                Skills = record.Skills.Select(skill => new SkillDto
                {
                    Name = skill.Name,
                    ResumeId = skill.ResumeId,
                    ProficiencyLevel = skill.ProficiencyLevel
                }).ToList(),
                Languages = record.Languages.Select(language => new LanguageDto
                {
                    Name = language.Name,
                    ProficiencyLevel = language.ProficiencyLevel,
                    ResumeId = language.ResumeId,
                }).ToList(),
                FullName = record.FullName,
                Email = record.Email,
                Phone = record.Phone,
                ProfilePictureUrl = record.ProfilePictureUrl,
                Summary = record.Summary,
                UserId = record.UserId,
            };
            return resumeDto;
        }
    }
}
