using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Customers.Queries;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Customers.Handlers.GetCustomerByIdHandler
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly IUnitOfWork _context;
        
        public GetCustomerByIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.CustomerRepository.GetQueryable()
                .Include(c => c.CustomerAddresses)  // Include the address
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        }
    }
}
