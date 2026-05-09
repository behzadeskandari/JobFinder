using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.CustomerAddress.Command
{
    public record UpdateCustomerAddressCommand(
        int Id,
        string Street,
        string City,
        string State,
        string PostalCode) : IRequest<bool>;
}
