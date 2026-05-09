using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Payment
{
    public record RequestPaymentDto(decimal Amount, string TestType);

    public class ZarinpalRequestResponse
    {
        public DataObj Data { get; set; }
        public object Errors { get; set; }
        public class DataObj { public string Authority { get; set; } }
    }

    public class ZarinpalVerifyResponse
    {
        public VerifyData Data { get; set; }
        public object Errors { get; set; }
        public class VerifyData { public int Code { get; set; } }
    }
}
