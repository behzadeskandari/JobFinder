using FluentResults;
using JobFinder.Application.Feature.Authentication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Common.Interfaces.Services
{
    public interface IAuthenticationCommandService
    {
        //OneOf<AuthenticationResult,IError> Register(string firstName, string lastName, string email, string password);
        Task<Result<AuthenticationResult>> Register(string firstName, string lastName, string email, string password);
    }
}
