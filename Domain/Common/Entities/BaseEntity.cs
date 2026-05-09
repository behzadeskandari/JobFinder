using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Domain.Common.Entities
{
    

    public interface ISoftDeletable
    {
        bool? IsActive { get; set; }
        DateTime? DateModified { get; set; }
    }

    public interface IBaseEntity<TKey> : ISoftDeletable
    {
        TKey Id { get; set; }
        DateTime? DateCreated { get; set; }
    }

    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
    {
        public virtual TKey Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
