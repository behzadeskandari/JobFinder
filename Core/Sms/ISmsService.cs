using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Sms
{
    internal interface ISmsService
    {
        Task<string> SendSmsAsync(string from, string to, string message);
        Task<string> SendSmsAsync(List<String> from, List<string> to, List<string> messages);

        Task<List<Kavenegar.Core.Models.SendResult>> SendBulkSmsAsync(string from, List<string> to, string message);

    }
}
