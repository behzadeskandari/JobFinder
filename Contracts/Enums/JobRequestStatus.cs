using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Enums
{
    public enum JobRequestStatus
    {
        Sended = 1,
        Read,
        Pending,
        Reviewed,
        Shortlisted,
        Interviewing,
        Offered,
        Rejected,
        Accepted,
    }
}
