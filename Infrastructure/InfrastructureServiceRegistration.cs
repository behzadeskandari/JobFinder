using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.Authentication;
using JobFinder.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureRegistrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuth();

            return services;
        }
        private static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtService>();

            return services;
        }
    }
}
