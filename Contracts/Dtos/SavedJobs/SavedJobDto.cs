using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.SavedJobs
{
    public class SavedJobDto
    {
        public int Id { get; set; }
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public string UserId { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }
    }
}
