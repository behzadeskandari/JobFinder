using JobFinder.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Job
{
    public class JobGetDto
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public JobLevel Level { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsPriority { get; set; }
        public JobType JobType { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirement { get; set; }
        public string CityName { get; set; }
        public string JobCategoryName { get; set; }
    }
}
