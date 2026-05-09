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
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, List<JobFinder.Domain.Common.Entities.Skill>>
    {
        private readonly IUnitOfWork _context;

        public GetAllSkillsHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<JobFinder.Domain.Common.Entities.Skill>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var record  = await _context.SkillsRepository.GetAllAsync();

            return record.ToList();
        }
    }
}
