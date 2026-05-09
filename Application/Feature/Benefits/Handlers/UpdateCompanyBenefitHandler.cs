using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Benefits.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace JobFinder.Application.Feature.Benefits.Handlers
{
    public class UpdateCompanyBenefitHandler : IRequestHandler<UpdateCompanyBenefitCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public UpdateCompanyBenefitHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCompanyBenefitCommand request, CancellationToken cancellationToken)
        {

            var company = await _context.companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                throw new NotFoundException("شرکت وجود ندارد. ابتدا شرکت را ایجاد کنید یا یکی را انتخاب کنید.");
            }
            var companyBenefit = await _context.CompanyBenefitsReposity.GetByIdAsync(request.Id);
            if (companyBenefit == null)
            {
                return false;
            }

            companyBenefit.CompanyId = request.CompanyId;
            companyBenefit.Name = request.Name;
            companyBenefit.Description = request.Description;
            companyBenefit.DateModified = DateTime.Now;
            companyBenefit.IsActive = request.IsActive;

            await _context.CompanyBenefitsReposity.UpdateAsync(companyBenefit);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
