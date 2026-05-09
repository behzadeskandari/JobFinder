using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Queries.SearchCompaniesQuery
{
    public class SearchCompaniesQuery : IRequest<IEnumerable<Company>>
    {
        public string SearchTerm { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
    }
}
