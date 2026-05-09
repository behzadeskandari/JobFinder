using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.Companies.Command.UpdateCompanyCommand;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Handler
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, bool>
    {
        private readonly ICompanyRepository _repository;

        public UpdateCompanyHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.GetByIdAsync(request.Id);

            if (company == null)
                return false;


            company.Name = request.Name;
            company.Logo = request.Logo;
            company.Description = request.Description;
            company.Industry = request.Industry;
            company.Location = request.Location;
            company.Website = request.Website;
            company.Size = request.CompanySize;
            company.FoundedDate = request.FoundedDate;
            company.IsVerified = request.IsVerified;
            company.ContactEmail = request.ContactEmail;
            company.ContactPhone = request.ContactPhone;
            company.IsActive = request.IsActive;

            // UpdateMBTI benefits
            company.Benefits.Clear();
            foreach (var benefit in request.Benefits)
            {
                company.Benefits.Add(new CompanyBenefit
                {
                    CompanyId = company.Id,
                    Name = benefit.Name,
                    Description = benefit.Description
                });
            }

            await _repository.UpdateAsync(company);
            return true;
        }
    }
}
