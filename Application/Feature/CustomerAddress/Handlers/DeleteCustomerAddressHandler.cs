using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.CustomerAddress.Command;
using MediatR;

namespace JobFinder.Application.Feature.CustomerAddress.Handlers
{

    public class DeleteCustomerAddressHandler : IRequestHandler<DeleteCustomerAddressCommand, bool>
    {
        private readonly IUnitOfWork _context;

        public DeleteCustomerAddressHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _context.CustomerAddressesRepository.GetByIdAsync(request.Id);
            if (address == null)
            {
                throw new NotFoundException("آدرس پیدا نشد");
            }

            await _context.CustomerAddressesRepository.DeleteAsync(address);
            await _context.CommitAsync(cancellationToken);
            return true;
        }
    }
}
