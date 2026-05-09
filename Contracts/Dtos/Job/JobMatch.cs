using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.JobPost;

namespace JobFinder.Contracts.Dtos.Job
{
    public class JobMatch
    {
        public JobPostDto JobPost { get; set; }
        public double Score { get; set; }
    }

}
