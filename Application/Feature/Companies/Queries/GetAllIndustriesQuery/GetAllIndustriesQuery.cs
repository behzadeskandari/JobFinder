using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Queries.GetAllIndustriesQuery
{
    public class GetAllIndustriesQuery : IRequest<IEnumerable<string>>
    {
    }
}
