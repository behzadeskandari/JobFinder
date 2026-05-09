using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Domain.Common.Entities
{

    public class Feature : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string IconName { get; set; }
        [Required]
        public string Language { get; set; }
        public DateTime? DateCreated { get ; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }

        // Navigation property for Jobs
        public ICollection<Job> Jobs { get; set; }
    }
}
