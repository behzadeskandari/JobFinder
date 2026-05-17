using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Roles
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Staff = "Staff";
        public const string User = "User";
        public const string All = $"{Admin},{Staff},{User}";

        public const string StaffAndAbove = $"{Admin},{Staff}";
        public const string AdminAndUser = $"{Admin},{User}";
        public const string StaffAndUser = $"{Admin},{User}";
    }
}
