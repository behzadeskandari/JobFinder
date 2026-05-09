using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.WriteRepository;
using JobFinder.Domain.Common.Entities;

namespace JobFinder.Application.Repository
{
    public interface IPersonalityTestResult :
        //IReadRepository<PersonalityTestResult>,
        IWriteRepository<PersonalityTestResult>
    {
        public Task<User> getPersonalityTestResultByUserId(string userId);
        public Task<User> getPersonalityTestResultByUserName(string userName);
    }
}
