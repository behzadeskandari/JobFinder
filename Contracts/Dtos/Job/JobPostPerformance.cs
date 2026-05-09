using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Job
{
    public class JobPostPerformance
    {
        public Guid JobPostId { get; set; }
        public string Title { get; set; }
        public int Views { get; set; }
        public int Applications { get; set; }
        // public TimeSpan? TimeToFill { get; set; }
        // public double ConversionRate { get; set; }
    }

}
