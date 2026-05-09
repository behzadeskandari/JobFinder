using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Benefits.Command;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Benefits.Handlers
{
    public class CreateCompanyBenefitHandler : IRequestHandler<CreateCompanyBenefitCommand, Guid>
    {
        private readonly IUnitOfWork _context;

        public CreateCompanyBenefitHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCompanyBenefitCommand request, CancellationToken cancellationToken)
        {

            var company = await _context.companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                throw new NotFoundException("شرکت وجود ندارد. ابتدا شرکت را ایجاد کنید یا یکی را انتخاب کنید.");
            }

            var companyBenefit = new CompanyBenefit
            {
                CompanyId = request.CompanyId,
                Name = request.Name,
                Description = request.Description,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _context.CompanyBenefitsReposity.AddAsync(companyBenefit);
            await _context.CommitAsync(cancellationToken);
            return companyBenefit.Id;
        }
    }
}
