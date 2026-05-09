using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;

namespace Persistance.Interfaces
{
    public interface IAuditableEntity : IBaseEntity<Guid>, IBaseEntity<int>
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? LastModifiedDate { get; set; }
        string? LastModifiedBy { get; set; }
        public string IpAddress { get; set; }
    }
}
