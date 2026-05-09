using System.ComponentModel.DataAnnotations;

namespace JobFinder.Contracts.Dtos.Account
{
    public class SendVerificationCodeRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
