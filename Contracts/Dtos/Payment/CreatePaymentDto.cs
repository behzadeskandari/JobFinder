using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Payment
{

    public class CreatePaymentDto
    {
        [Required]
        public int AdvertisementId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string CallbackUrl { get; set; }
    }
}
