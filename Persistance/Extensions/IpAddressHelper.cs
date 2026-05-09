using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Persistance.Extensions
{
    public static class IpAddressHelper
    {
        public static string GetClientIp(this HttpContext context)
        {
            // Check if forwarded header exists (used in reverse proxy)
            var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!string.IsNullOrEmpty(forwardedHeader))
            {
                // Might contain multiple addresses: client, proxy1, proxy2, ...
                return forwardedHeader.Split(',')[0].Trim();
            }

            // Fall back to remote IP address
            return context.Connection.RemoteIpAddress?.ToString();
        }
    }
}
