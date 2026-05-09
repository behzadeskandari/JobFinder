using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Customers.Command;
using JobFinder.Contracts.Dtos.Customer;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Customers.Handlers.CreateCutomerHandler
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Result<Customer>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Customer>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerData = SerializeCustomer(request.Customer);

            var isExists =_unitOfWork.CustomerRepository.IsExists(customerData.Id).GetAwaiter().IsCompleted;

            if (isExists)
            {
                throw new NotFoundException("مشتری از قبل وجود دارد");
            }
            var newCustomer = await _unitOfWork.CustomerRepository.AddAsync(customerData);
            await _unitOfWork.CommitAsync(cancellationToken);
            if (newCustomer != null)
            {
                return Result.Ok(newCustomer);
            }

            throw new NotFoundException("مشتری پیدا نشد");
        }

        private Customer SerializeCustomer(CustomerDto customer)
        {
            return new Customer
            {
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                CustomerAddresses = MapCustomerAddressToEntity(customer.PrimaryAddress),
            };
        }

        private static List<JobFinder.Domain.Common.Entities.CustomerAddress> MapCustomerAddressToEntity(List<CustomerAddressDto> address)
        {
            var lst = new List<JobFinder.Domain.Common.Entities.CustomerAddress>();
            foreach (var item in address)
            {
                lst.Add(new JobFinder.Domain.Common.Entities.CustomerAddress
                {
                    AddressLine1 = item.AddressLine1,
                    AddressLine2 = item.AddressLine2,
                    City = item.City,
                    State = item.State,
                    PostalCode = item.PostalCode,
                    Country = item.Country,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                });
            }
            return lst;
        }
    }
}
