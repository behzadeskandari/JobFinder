using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CustomerAddress.Command;
using MediatR;

namespace JobFinder.Application.Feature.CustomerAddress.Handlers
{

    public class CreateCustomerAddressHandler : IRequestHandler<CreateCustomerAddressCommand, Guid>
    {
        private readonly IUnitOfWork _context;

        public CreateCustomerAddressHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var address = new JobFinder.Domain.Common.Entities.CustomerAddress
            {
                CustomerId = request.CustomerId,
                Street = request.Street,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode
            };

            await _context.CustomerAddressesRepository.AddAsync(address);
            await _context.CommitAsync(cancellationToken);
            return address.Id;
        }
    }
}
