using FluentResults;
using JobFinder.Application.Common.Interfaces.Authentication;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Feature.Authentication.Common;
using JobFinder.Application.Services;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Authentication.Command
{
    public class AuthenticationCommandService : IAuthenticationCommandService
    {
        public readonly IJwtTokenGenerator _jwtTokenGenerator;
        // public readonly IUserRepository _userRepository;
        ///, IUserRepository userRepository
        public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            //_userRepository = userRepository;
        }


        //OneOf<AuthenticationResult, IError>
        public async Task<Result<AuthenticationResult>> Register(string firstName, string lastName, string email, string password)
        {
            //1 validate if user exists 
            //if (_userRepository.GetUserByEmail(email) is not null)
              //  return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
            //throw new DuplicateEmailException();//"User With given Email Already Exists"

            var user = new User() { Email = email, Password = password, FirstName = firstName, LastName = lastName };

            //_userRepository.Add(user);

            var token = await _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }


    }

}
