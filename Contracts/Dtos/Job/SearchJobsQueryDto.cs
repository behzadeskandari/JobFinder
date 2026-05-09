using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Job
{
    public class SearchJobsQueryDto
    {
        public int technicalOptions { get; set; }
        public int jobCategory { get; set; }
        public int? city { get; set; }
        public int province { get; set; }

        public int PageNumber { get;  set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
