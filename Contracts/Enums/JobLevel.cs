using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Enums
{
    public enum JobLevel
    {
        Intern,
        Junior,
        MidLevel,
        Senior,
        TeamLead,
        Cto,
        Architect
    }


    public enum JobType
    {
        PartTime = 1,
        HalfTime = 2,
        FullTime = 3,
    }
}
