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
    public class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteSkillHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _context.SkillsRepository.GetByIdAsync(request.Id);
            if (skill == null)
            {
                throw new NotFoundException("مهارت مورد نظر پیدا نشد");
            }

            await _context.SkillsRepository.DeleteAsync(skill);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
