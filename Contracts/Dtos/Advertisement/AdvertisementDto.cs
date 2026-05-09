using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Account;
using JobFinder.Contracts.Dtos.Category;
using JobFinder.Contracts.Dtos.Company;
using JobFinder.Contracts.Dtos.Payment;

namespace JobFinder.Contracts.Dtos.Advertisement
{

    public class AdvertisementDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string StaffId { get; set; }

        public UserDto? Staff { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public CategoryDto? Category { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public CompanyGetDto? Company { get; set; }

        public DateTime JobADVCreatedAt { get; set; } = DateTime.Now;

        public DateTime? ExpiresAt { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsPaid { get; set; } = false;

        public PaymentDto? Payment { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public string? StaffEmail { get; set; }
        public string? CategoryName { get; set; }
    }
}
