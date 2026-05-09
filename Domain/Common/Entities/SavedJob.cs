using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Domain.Common.Entities
{
    public class SavedJob : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid? JobId { get; set; }
        public Job? Job { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
