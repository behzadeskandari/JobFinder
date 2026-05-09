using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Companies.Queries.GetCompanyByIdQuery;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Handler
{
    public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, Company>
    {
        private readonly IUnitOfWork _repository;

        public GetCompanyByIdHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Company> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            Company record =  await _repository.companyRepository.GetByIdAsync(request.Id);
            var benefit = _repository.CompanyBenefitsReposity.GetByCompanyId(request.Id);
            record.Benefits.ToList().AddRange(benefit);
            return record;
        }
    }
}
