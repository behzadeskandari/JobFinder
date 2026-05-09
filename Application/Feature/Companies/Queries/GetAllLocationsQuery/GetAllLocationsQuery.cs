using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Queries.GetAllLocationsQuery
{
    public class GetAllLocationsQuery : IRequest<IEnumerable<string>>
    {
    }
}
