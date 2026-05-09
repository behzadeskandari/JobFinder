using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Customer;
using JobFinder.Domain.Common.Entities;

namespace Application.MappingProfile
{
    public static class CustomerMapper
    {
        /// <summary>
        /// Serializes a Customer data model into a CustomerModel view model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static CustomerDto SerializeCustomer(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.CustomerAddresses),
            };
        }

        private static List<CustomerAddressDto> MapCustomerAddress(ICollection<CustomerAddress> customerAddresses)
        {

            if (customerAddresses == null)
            {
                return new List<CustomerAddressDto>(); // Or throw an exception, depending on your design
            }

            return customerAddresses.Select(address => new CustomerAddressDto
            {
                Id = address.Id,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country,
                CustomerId = address.CustomerId, // Include the CustomerId
                Street = address.Street
            }).ToList();
        }

        /// <summary>
        /// Serializes a CustomerModel view model into a Customer data model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Customer SerializeCustomer(CustomerDto customer)
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

        /// <summary>
        /// Maps a CustomerAddress data model to a CustomerAddressModel view model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static List<CustomerAddressDto> MapCustomerAddress(CustomerAddress address)
        {
            return new List<CustomerAddressDto>
            {
                new CustomerAddressDto()
                {
                    Id = address.Id,
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    City = address.City,
                    State = address.State,
                    PostalCode = address.PostalCode,
                    Country = address.Country,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                }
            };
        }



        public static List<CustomerAddressDto> MapCustomerAddress(List<CustomerAddress> addresses)
        {
            if (addresses == null)
            {
                return new List<CustomerAddressDto>(); // Or throw an exception, depending on your design
            }

            return addresses.Select(address => new CustomerAddressDto
            {
                Id = address.Id,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country,
                CustomerId = address.CustomerId, // Include the CustomerId
                Street = address.Street
            }).ToList();
        }

        /// <summary>
        /// Maps a CustomerAddressModel view model to a CustomerAddress data model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static List<CustomerAddress> MapCustomerAddress(CustomerAddressDto address)
        {
            return new List<CustomerAddress> {

                new CustomerAddress
                {
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    City = address.City,
                    State = address.State,
                    PostalCode = address.PostalCode,
                    Country = address.Country,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                }
             };
        }
        public static List<CustomerAddress> MapCustomerAddressToEntity(List<CustomerAddressDto> address)
        {
            var lst = new List<CustomerAddress>();
            foreach (var item in address)
            {
                lst.Add(new CustomerAddress
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
