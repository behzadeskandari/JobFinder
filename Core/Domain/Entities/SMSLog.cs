using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class SMSLog
    {
        public List<string> from { get; set; }
        public List<string> to { get; set; }
        public List<string> messages { get; set; }
        public int Id { get; set; }

    }


}
