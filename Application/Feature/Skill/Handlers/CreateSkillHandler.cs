using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Skill.Command;
using MediatR;

namespace JobFinder.Application.Feature.Skill.Handlers
{
    public class CreateSkillHandler : IRequestHandler<CreateSkillCommand, int>
    {
        private readonly IUnitOfWork _context;

        public CreateSkillHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {

            var candidate = await _context.CandidateRepository.GetByIdAsync(request.CandidateId);

            if (candidate == null) 
            {
                throw new NotFoundException("کاندیدا پیدا نشد");
            }

            var resume = await _context.ResumeRepository.GetByIdAsync(request.ResumeId);
            
            if(resume == null)
            {
                throw new NotFoundException("رزومه پیدا نشد");
            }
            
            var skill = new JobFinder.Domain.Common.Entities.Skill
            {
                ResumeId = resume.Id,
                Resume = resume,
                Name = request.Name,
                ProficiencyLevel = request.ProficiencyLevel,
                DateCreated = DateTime.Now,
                IsActive = true,
                CandidateId = candidate.Id,
                Candidates = candidate,
            };

            await _context.SkillsRepository.AddAsync(skill);
            await _context.CommitAsync(cancellationToken);
            return skill.Id;
        }
    }
}
