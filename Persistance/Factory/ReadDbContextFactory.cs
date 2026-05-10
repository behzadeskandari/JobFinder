using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistance.DatabaseContext.ReadDbContext;

namespace Persistance.Factory
{
    internal class ReadDbContextFactory : IDesignTimeDbContextFactory<ReadDbContext>
    {
        public ReadDbContext CreateDbContext(string[] args)
        {

            Console.WriteLine($"Creating ReadDbContext...{Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "JobFinder"))}");
            // بارگذاری کانفیگ از appsettings.json یا appsettings.Development.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "JobFinder")))
                .AddJsonFile("appsettings.Development.json", optional: true)
                 .AddJsonFile("appsettings.json", optional: true)
                .Build();


            var builder = new DbContextOptionsBuilder<ReadDbContext>();
            var connectionString = configuration.GetConnectionString("WorkReadDB");

            builder.UseSqlServer(connectionString);

            Console.WriteLine($"Created ReadDbContext...{Directory.GetCurrentDirectory()}");
            return new ReadDbContext(builder.Options, configuration);
        }
    }
}
