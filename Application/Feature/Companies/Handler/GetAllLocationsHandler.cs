using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.Companies.Queries.GetAllLocationsQuery;
using JobFinder.Application.Repository;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Handler
{
    public class GetAllLocationsHandler : IRequestHandler<GetAllLocationsQuery, IEnumerable<string>>
    {
        private readonly ICompanyRepository _repository;

        public GetAllLocationsHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllLocationsAsync();
        }
    }
}
