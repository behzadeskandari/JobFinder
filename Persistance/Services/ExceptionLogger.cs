using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistance.DatabaseContext.LogContext;
using Persistance.Interfaces;

namespace Persistance.Services
{
    public class ExceptionLogger : IExceptionLogger
    {
        private readonly Persistance.DatabaseContext.LogContext.ExceptionContext _context;

        public ExceptionLogger(Persistance.DatabaseContext.LogContext.ExceptionContext context)
        {
            _context = context;
        }

        public async Task LogAsync(Exception ex)
        {
            var st = new StackTrace(ex, true);
            var frame = st.GetFrames()?.FirstOrDefault(f => f.GetFileLineNumber() > 0);

            var log = new ExceptionLog
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source,
                ExceptionType = ex.GetType().FullName,
                DateCreated = DateTime.Now,
                ClassName = frame?.GetMethod()?.DeclaringType?.FullName,
                MethodName = frame?.GetMethod()?.Name,
            };

            _context.ExceptionLog.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
