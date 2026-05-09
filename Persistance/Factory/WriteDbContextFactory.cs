using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistance.DatabaseContext.WriteDbContext;

namespace Persistance.Factory
{
    public class WriteDbContextFactory : IDesignTimeDbContextFactory<WriteDbContext>
    {
        public WriteDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine($"Creating WriteDbContext...{Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "JobSeeker.Api"))}");
            // بارگذاری کانفیگ از appsettings.json یا appsettings.Development.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "JobSeeker.Api")))
                .AddJsonFile("appsettings.Development.json", optional: true)
                 .AddJsonFile("appsettings.json", optional: true)
                .Build();


            var builder = new DbContextOptionsBuilder<WriteDbContext>();
            var connectionString = configuration.GetConnectionString("WorkWriteDB");

            builder.UseSqlServer(connectionString);

            Console.WriteLine($"Created WriteDbContext...{Directory.GetCurrentDirectory()}");
            return new WriteDbContext(builder.Options, configuration);
        }
    }


}
