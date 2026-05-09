using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Payment
{
    public class VerifyPaymentDto
    {
        [Required]
        public string Authority { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
