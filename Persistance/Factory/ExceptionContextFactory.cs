using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistance.Factory
{
    internal class ExceptionContextFactory : IDesignTimeDbContextFactory<Persistance.DatabaseContext.LogContext.ExceptionContext>
    {
        public Persistance.DatabaseContext.LogContext.ExceptionContext CreateDbContext(string[] args)
        {

            Console.WriteLine($"Creating ExceptionContext...{Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "JobSeeker.Api"))}");
            // بارگذاری کانفیگ از appsettings.json یا appsettings.Development.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "JobSeeker.Api")))
                .AddJsonFile("appsettings.Development.json", optional: true)
                 .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<Persistance.DatabaseContext.LogContext.ExceptionContext>();
            var connectionString = configuration.GetConnectionString("WorkLoggingConnection");

            builder.UseSqlServer(connectionString);

            Console.WriteLine($"Created ExceptionContext...{Directory.GetCurrentDirectory()}");
            return new Persistance.DatabaseContext.LogContext.ExceptionContext(builder.Options);
        }
    }
}
