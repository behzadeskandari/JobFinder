using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.Companies.Queries.GetAllIndustriesQuery;
using JobFinder.Application.Repository;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Handler
{
    public class GetAllIndustriesHandler : IRequestHandler<GetAllIndustriesQuery, IEnumerable<string>>
    {
        private readonly ICompanyRepository _repository;

        public GetAllIndustriesHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> Handle(GetAllIndustriesQuery request, CancellationToken cancellationToken)
        {
            var r =  await _repository.GetAllIndustriesAsync();
            return r;
        }
    }
}
