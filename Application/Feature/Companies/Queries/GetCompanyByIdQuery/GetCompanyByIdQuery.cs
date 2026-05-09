using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Queries.GetCompanyByIdQuery
{
    public class GetCompanyByIdQuery : IRequest<Company>
    {
        public Guid Id { get; set; }
    }
}
