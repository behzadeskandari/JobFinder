using Application.MappingProfile;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Order.Command.GenerateNewOrderCommand;
using JobFinder.Application.Repository.Invoice;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Order.Handlers.GenerateNewOrderHandler
{
    public class GenerateNewOrderHandler : IRequestHandler<GenerateNewOrderCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateNewOrderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GenerateNewOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = OrderMapper.SerializeInvoiceToOrder(request.Invoice);
                var customerResult =  await _unitOfWork.CustomerRepository.GetByIdAsync(request.Invoice.CustomerId);

                if (customerResult == null)
                {
                    throw new NotFoundException("مشتری پیدا نشد");
                }
                List<JobFinder.Domain.Common.Entities.CustomerAddress> customerAddresslist = new List<JobFinder.Domain.Common.Entities.CustomerAddress>();
                foreach (var item in customerResult.CustomerAddresses)
                {
                    var record = new JobFinder.Domain.Common.Entities.CustomerAddress()
                    {
                        AddressLine1 = item.AddressLine1,
                        AddressLine2 = item.AddressLine2,
                        UpdatedOn = item.UpdatedOn,
                        City = item.City,
                        Country = item.Country,
                        CreatedOn = item.CreatedOn,
                        Id = item.Id,
                        PostalCode = item.PostalCode,
                        State = item.State
                    };
                    customerAddresslist.Add(record);
                }

                order.Customer = new Customer()
                {
                    CreatedOn = customerResult.CreatedOn,
                    FirstName = customerResult.FirstName,
                    Id = customerResult.Id,
                    LastName = customerResult.LastName,
                    UpdatedOn = customerResult.UpdatedOn,
                    CustomerAddresses = customerAddresslist 
                };

                var orderResult =  _unitOfWork.OrderRepository.GenerateOpenOrder(order);

                if (orderResult.IsSuccess)
                {
                    return Result.Ok();
                }
                else
                {
                    throw new NotFoundException(orderResult.Errors.ToString());
                }
            }
            catch (Exception e)
            {
                return Result.Fail(new Error(e.Message));
            }
        }
    }
}
