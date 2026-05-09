using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Exceptions;

namespace JobFinder.Application.Common.Exceptions
{
    public sealed class UnauthorizedActionException : BadRequestException
    {
        public UnauthorizedActionException(string message) : base(message) { }
    }

}
