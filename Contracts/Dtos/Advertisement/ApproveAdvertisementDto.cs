using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Advertisement
{
    public class ApproveAdvertisementDto
    {
        [Required]
        public bool IsApproved { get; set; }
    }
}
