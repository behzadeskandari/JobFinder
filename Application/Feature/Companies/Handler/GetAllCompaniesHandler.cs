using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.Companies.Queries.GetAllCompaniesQuery;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Handler
{
    public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<Company>>
    {
        private readonly ICompanyRepository _repository;

        public GetAllCompaniesHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var record =  await _repository.GetAllAsync();
            return record;
        }
    }
}
