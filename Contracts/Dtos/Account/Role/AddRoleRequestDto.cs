using System.ComponentModel.DataAnnotations;

namespace JobFinder.Contracts.Dtos.Account.Role
{
    public class AddRoleRequestDto
    {
        [Required]
        public string Role { get; set; }
    }
}
