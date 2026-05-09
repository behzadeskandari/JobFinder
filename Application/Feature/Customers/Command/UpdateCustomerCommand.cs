using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Customers.Command
{
    public record UpdateCustomerCommand(
     Guid Id,
     string FirstName,
     string LastName,
     string Street,
     string City,
     string State,
     string PostalCode,
      Guid OrdersId,
     DateTime UpdatedOn,
     bool? IsActive) : IRequest<bool>
    {
        public string CustomerType { get; set; }
    }
}
