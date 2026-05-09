using FluentResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Domain.Common.Errors
{
    public class HandlerValidationException : ValidationException, IError
    {
        public HandlerValidationException(string message)
            : base(message)
        {
        }

        public List<IError> Reasons { get; set; }

        public Dictionary<string, object> Metadata { get; set; }
    }
}
