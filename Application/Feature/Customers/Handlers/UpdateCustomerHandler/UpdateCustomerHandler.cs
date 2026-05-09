using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Customers.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Customers.Handlers.UpdateCustomerHandler
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public UpdateCustomerHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.CustomerRepository.GetQueryable()
                .Include(c => c.CustomerAddresses) // Include the address
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customer == null)
            {
                throw new NotFoundException("مشتری پیدا نشد");
            }

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.UpdatedOn = DateTime.Now;
            customer.IsActive = request.IsActive;
            customer.OrdersId = request.OrdersId;

            // Update the address
            if (customer.CustomerAddresses != null)
            {
                foreach (var item in customer.CustomerAddresses)
                {
                    item.Street = request.Street;
                    item.City = request.City;
                    item.State = request.State;
                    item.PostalCode = request.PostalCode;

                }
            }
            else //if the address is null, create a new
            {
                customer.CustomerAddresses = new List<JobFinder.Domain.Common.Entities.CustomerAddress>
                {
                    new JobFinder.Domain.Common.Entities.CustomerAddress
                    {
                        Street = request.Street,
                        City = request.City,
                        State = request.State,
                        PostalCode = request.PostalCode,
                        CustomerId = customer.Id
                    }
                };
                await _context.CustomerAddressesRepository.AddRangeAsync(customer.CustomerAddresses);
            }

            await _context.CustomerRepository.UpdateAsync(customer);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }

}
