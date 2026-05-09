using System.ComponentModel.DataAnnotations;

namespace JobFinder.Contracts.Dtos.Account
{
    public class VerifyEmailRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
