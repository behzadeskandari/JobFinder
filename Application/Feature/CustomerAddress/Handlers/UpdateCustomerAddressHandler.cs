using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CustomerAddress.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.CustomerAddress.Handlers
{
    public class UpdateCustomerAddressHandler : IRequestHandler<UpdateCustomerAddressCommand, bool>
    {
        private readonly IUnitOfWork _context;
        
        public UpdateCustomerAddressHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _context.CustomerAddressesRepository.GetByIdAsync(request.Id);
            if (address == null)
            {
                throw new NotFoundException("آدرس پیدا نشد");
            }

            address.Street = request.Street;
            address.City = request.City;
            address.State = request.State;
            address.PostalCode = request.PostalCode;

            await _context.CustomerAddressesRepository.UpdateAsync(address);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
