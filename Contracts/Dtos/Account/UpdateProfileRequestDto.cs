using System.ComponentModel.DataAnnotations;

namespace JobFinder.Contracts.Dtos.Account
{
    public class UpdateProfileRequestDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        
        // Add other profile fields as needed
    }
}
