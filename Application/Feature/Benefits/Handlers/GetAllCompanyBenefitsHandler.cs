using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Benefits.Queries;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Benefits.Handlers
{

    public class GetAllCompanyBenefitsHandler : IRequestHandler<GetAllCompanyBenefitsQuery, List<CompanyBenefit>>
    {
        private readonly IUnitOfWork _context;

        public GetAllCompanyBenefitsHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<CompanyBenefit>> Handle(GetAllCompanyBenefitsQuery request, CancellationToken cancellationToken)
        {
            return await _context.CompanyBenefitsReposity.GetQueryable()
                            //.Include(cb => cb.Company) // Eager load the Company
                            .ToListAsync(cancellationToken);
        }
    }

}
