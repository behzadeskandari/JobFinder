using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Skill.Queries;
using MediatR;

namespace JobFinder.Application.Feature.Skill.Handlers
{
    public class GetSkillByIdHandler : IRequestHandler<GetSkillByIdQuery, JobFinder.Domain.Common.Entities.Skill>
    {
        private readonly IUnitOfWork _context;

        public GetSkillByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.Skill> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.SkillsRepository.GetByIdAsync(request.Id);
        }
    }

}
