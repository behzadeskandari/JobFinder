using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.Companies.Queries.SearchCompaniesQuery;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Handler
{
    public class SearchCompaniesHandler : IRequestHandler<SearchCompaniesQuery, IEnumerable<Company>>
    {
        private readonly ICompanyRepository _repository;

        public SearchCompaniesHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Company>> Handle(SearchCompaniesQuery request, CancellationToken cancellationToken)
        {
            var r =  await _repository.SearchAsync(request.SearchTerm);
            return r;
        }
    }
}
