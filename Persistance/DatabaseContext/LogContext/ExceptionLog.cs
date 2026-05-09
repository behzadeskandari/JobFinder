using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;

namespace Persistance.DatabaseContext.LogContext
{
    public class ExceptionLog : IBaseEntity<int>
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string ExceptionType { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
    }
}
