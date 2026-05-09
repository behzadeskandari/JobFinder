using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Job
{

    public class TopJobCategory
    {
        public string CategoryName { get; set; }
        public int TotalApplications { get; set; }
    }
}
