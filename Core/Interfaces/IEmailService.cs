using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailMessage message);
        Task<bool> SendEmailAsync(string to, string subject, string body, List<string>? attachments = null);
        Task<bool> SendBulkEmailAsync(List<EmailMessage> messages);
    }
}
