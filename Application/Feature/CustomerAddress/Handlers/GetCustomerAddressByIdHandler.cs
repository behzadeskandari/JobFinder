using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CustomerAddress.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.CustomerAddress.Handlers
{
    public class GetCustomerAddressByIdHandler : IRequestHandler<GetCustomerAddressByIdQuery,JobFinder.Domain.Common.Entities.CustomerAddress>
    {
        private readonly IUnitOfWork _context;

        public GetCustomerAddressByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<JobFinder.Domain.Common.Entities.CustomerAddress> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.CustomerAddressesRepository.GetQueryable().FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        }
    }
}
