using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{

    public class SendResult
    {
        public int Id { get; set; }
        public long Messageid { get; set; }

        public int Cost { get; set; }

        public long Date { get; set; }

        public string Message { get; set; }

        public string Receptor { get; set; }

        public string Sender { get; set; }

        public int Status { get; set; }

        public string StatusText { get; set; }
    }
}
