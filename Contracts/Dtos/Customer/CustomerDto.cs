using JobFinder.Contracts.Dtos.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Customer
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(32)] public string FirstName { get; set; }
        [MaxLength(32)] public string LastName { get; set; }
        public List<CustomerAddressDto> PrimaryAddress { get; set; }
        public string CustomerType { get; set; }
        public int UserId { get; set; }
        public int? OrdersId { get; set; }
        public OrderDto? Orders { get; set; }
    }
}
