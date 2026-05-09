using JobFinder.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.JobOffer
{
    public class UpdateJobOfferStatusDto
    {
        [Required]
        public JobOfferStatus Status { get; set; }
    }
}
