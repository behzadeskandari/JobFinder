using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Authentication.Common
{
    public record AuthenticationResult(User User, string Token, IEnumerable<string> errors = null);

}
