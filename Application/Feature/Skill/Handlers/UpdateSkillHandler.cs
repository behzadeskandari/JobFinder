using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Skill.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Skill.Handlers
{
    public class UpdateSkillHandler : IRequestHandler<UpdateSkillCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public UpdateSkillHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _context.SkillsRepository.GetByIdAsync(request.Id);
            if (skill == null)
            {
                throw new NotFoundException("مهارت پیدا نشد");
            }

            skill.ResumeId = request.ResumeId;
            skill.Name = request.Name;
            skill.ProficiencyLevel = request.ProficiencyLevel;
            skill.DateModified = DateTime.Now;
            skill.IsActive = request.IsActive;

            await _context.SkillsRepository.UpdateAsync(skill);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
