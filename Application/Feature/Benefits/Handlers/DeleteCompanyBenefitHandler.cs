using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Benefits.Command;
using MediatR;

namespace JobFinder.Application.Feature.Benefits.Handlers
{

    public class DeleteCompanyBenefitHandler : IRequestHandler<DeleteCompanyBenefitCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteCompanyBenefitHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCompanyBenefitCommand request, CancellationToken cancellationToken)
        {
            var companyBenefit = await _context.CompanyBenefitsReposity.GetByIdAsync(request.Id);
            if (companyBenefit == null)
            {
                return false;
            }

            await _context.CompanyBenefitsReposity.DeleteAsync(companyBenefit);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }

}
