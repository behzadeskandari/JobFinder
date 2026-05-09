using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Candidate
{
    public class ApplicantLocation
    {
        public int? CityId { get; set; }
        public int ApplicantCount { get; set; }
    }
}
