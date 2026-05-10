using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DomainServiceRegistration
    {
        public static IServiceCollection ConfigureDomainRegistrationServices(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }

    }
}
